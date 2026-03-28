using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalamityModClassicPreTrailer.BiomeManagers;
using CalamityModClassicPreTrailer.Dusts;
using CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.AstralBiomeNPCs
{
    public class Nova : ModNPC
    {
        private static Texture2D glowmask;

        private const float travelAcceleration = 0.2f;
        private const float targetTime = 120f;
        private const float waitBeforeTravel = 20f;
        private const float maxTravelTime = 300f;
        private const float slowdown = 0.84f;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Nova");
            Main.npcFrameCount[NPC.type] = 8;

            if (!Main.dedServ)
                glowmask = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AstralBiomeNPCs/NovaGlow").Value;
        }
        
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                new FlavorTextBestiaryInfoElement("Simple creatures lacking in means of offense or defense, instead they charge at enemies and self destruct in order to defend the infection.")
            });
        }

        public override void SetDefaults()
        {
            NPC.width = 78;
            NPC.height = 50;
            NPC.damage = 75;
            NPC.defense = 25;
            NPC.lifeMax = 350;
            NPC.DeathSound = new SoundStyle("CalamityModClassicPreTrailer/Sounds/NPCKilled/AstralEnemyDeath");
            NPC.noGravity = true;
            NPC.knockBackResist = 0.4f;
            NPC.value = Item.buyPrice(0, 0, 20, 0);
            NPC.aiStyle = -1;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("NovaBanner").Type;
            SpawnModBiomes = new int[] { ModContent.GetInstance<Astral>().Type };
		}

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.ai[3] >= 0f)
            {
                if (NPC.frameCounter >= 8)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y += frameHeight;
                    if (NPC.frame.Y >= frameHeight * 4)
                    {
                        NPC.frame.Y = 0;
                    }
                }
            }
            else
            {
                if (NPC.frameCounter >= 7)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y += frameHeight;
                    if (NPC.frame.Y >= frameHeight * 8)
                    {
                        NPC.frame.Y = frameHeight * 4;
                    }
                }
            }
            
            //DO DUST
            Dust d = CalamityGlobalNPC.SpawnDustOnNPC(NPC, 114, frameHeight, ModContent.DustType<AstralOrange>(), new Rectangle(78, 34, 36, 18), Vector2.Zero, 0.45f, true);
            if (d != null)
            {
                d.customData = 0.04f;
            }
        }

        public override void AI()
        {
            Player target = Main.player[NPC.target];
            if (NPC.ai[3] >= 0)
            {
                CalamityGlobalNPC.DoFlyingAI(NPC, 5.5f, 0.035f, 400f, 150, false);

                if (Collision.CanHit(NPC.position, NPC.width, NPC.height, target.position, target.width, target.height))
                {
                    NPC.ai[3]++;
                }
                else
                {
                    NPC.ai[3] = 0f;
                }

                Vector2 between = target.Center - NPC.Center;

                //after locking target for x amount of time and being far enough away
                if (between.Length() > 150 && NPC.ai[3] >= targetTime && Main.rand.Next(180) == 0) 
                {
                    //set ai mode to target and travel
                    NPC.ai[3] = -1f;
                }
                return;
            }
            else
            {
                NPC.ai[3]--;
                Vector2 between = target.Center - NPC.Center;

                if (NPC.ai[3] < -waitBeforeTravel)
                {
                    if (NPC.collideX || NPC.collideY || NPC.ai[3] < -maxTravelTime)
                    {
                        Explode();
                    }
                    
                    NPC.velocity += new Vector2(NPC.ai[1], NPC.ai[2]) * travelAcceleration; //acceleration per frame
                    
                    //rotation
                    NPC.rotation = NPC.velocity.ToRotation();
                }
                else if (NPC.ai[3] == -waitBeforeTravel)
                {
                    between.Normalize();
                    NPC.ai[1] = between.X;
                    NPC.ai[2] = between.Y;

                    //rotation
                    NPC.rotation = between.ToRotation();
                    NPC.velocity = Vector2.Zero;
                }
                else
                {
                    //slowdown
                    NPC.velocity *= slowdown;

                    //rotation
                    NPC.rotation = between.ToRotation();
                }
                NPC.rotation += MathHelper.Pi;
            }
        }

        private void Explode()
        {
            //kill NPC
            SoundEngine.PlaySound(SoundID.Item14, NPC.Center);

            //change stuffs
            Vector2 center = NPC.Center;
            NPC.width = 200;
            NPC.height = 200;
            NPC.Center = center;

            Rectangle myRect = NPC.getRect();

            if (Main.netMode != 1)
            {
                for (int i = 0; i < 200; i++)
                {
                    if (Main.player[i].getRect().Intersects(myRect))
                    {
                        int direction = NPC.Center.X - Main.player[i].Center.X < 0 ? -1 : 1;
                        Main.player[i].Hurt(PlayerDeathReason.ByNPC(NPC.whoAmI), 100, direction);
                    }
                }
            }

            //other things
            NPC.ai[3] = -20000f;
            NPC.value = 0f;
            NPC.extraValue = 0;
            if (Main.netMode != NetmodeID.MultiplayerClient)
                NPC.StrikeInstantKill();

            int size = 30;
            Vector2 off = new Vector2(size / -2f);

            for (int i = 0; i < 45; i++)
            {
                int dust = Dust.NewDust(NPC.Center - off, size, size, ModContent.DustType<AstralEnemy>(), Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 0, default(Color), Main.rand.NextFloat(1f, 2f));
                Main.dust[dust].velocity *= 1.4f;
            }
            for (int i = 0; i < 15; i++)
            {
                int dust = Dust.NewDust(NPC.Center - off, size, size, 31, 0f, 0f, 100, default(Color), 1.7f);
                Main.dust[dust].velocity *= 1.4f;
            }
            for (int i = 0; i < 27; i++)
            {
                int dust = Dust.NewDust(NPC.Center - off, size, size, 6, 0f, 0f, 100, default(Color), 2.4f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 5f;
                dust = Dust.NewDust(NPC.Center - off, size, size, 6, 0f, 0f, 100, default(Color), 1.6f);
                Main.dust[dust].velocity *= 3f;
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.soundDelay == 0)
            {
                NPC.soundDelay = 15;
                switch (Main.rand.Next(3))
                {
                    case 0:
                        SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/NPCHit/AstralEnemyHit"), NPC.Center);
                        break;
                    case 1:
                        SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/NPCHit/AstralEnemyHit2"), NPC.Center);
                        break;
                    case 2:
                        SoundEngine.PlaySound(new SoundStyle("CalamityModClassicPreTrailer/Sounds/NPCHit/AstralEnemyHit3"), NPC.Center);
                        break;
                }
            }

            CalamityGlobalNPC.DoHitDust(NPC, hit.HitDirection, (Main.rand.Next(0, Math.Max(0, NPC.life)) == 0) ? 5 : ModContent.DustType<AstralEnemy>(), 1f, 3, 40);

            //if dead do gores
            if (NPC.life <= 0)
            {
                if (Main.netMode != NetmodeID.Server)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        Gore.NewGore(NPC.GetSource_FromThis(null), NPC.Center, NPC.velocity * 0.3f,
                            Mod.Find<ModGore>("NovaGore" + i).Type);
                    }
                }
            }
        }

        public override bool PreKill()
        {
            return NPC.ai[3] > -10000;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            spriteBatch.Draw(glowmask, NPC.Center - Main.screenPosition - new Vector2(0, 4f), NPC.frame, Color.White * 0.75f, NPC.rotation, new Vector2(57f, 37f), NPC.scale, NPC.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
        }
        
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneAstral && spawnInfo.Player.ZoneOverworldHeight)
            {
                return 0.19f;
            }
            return 0f;
        }
        
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("Stardust").Type, 1, 2, 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), Mod.Find<ModItem>("Stardust").Type, 1));
            npcLoot.Add(ItemDropRule.ByCondition(new DownedAstrumDeus(), Mod.Find<ModItem>("StellarCannon").Type, 7));
        }
    }
}
