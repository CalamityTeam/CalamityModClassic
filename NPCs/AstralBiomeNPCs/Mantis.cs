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
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.NPCs.AstralBiomeNPCs
{
    public class Mantis : ModNPC
    {
        private static Texture2D glowmask;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mantis");
            Main.npcFrameCount[NPC.type] = 14;
            if (!Main.dedServ)
                glowmask = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AstralBiomeNPCs/MantisGlow").Value;
        }

        public override void SetDefaults()
        {
            NPC.damage = 85;
            NPC.width = 60;
            NPC.height = 58;
            NPC.aiStyle = -1;
            NPC.defense = 16;
            NPC.lifeMax = 510;
            NPC.knockBackResist = 0.1f;
            NPC.value = Item.buyPrice(0, 0, 15, 0);
            NPC.DeathSound = new SoundStyle("CalamityModClassicPreTrailer/Sounds/NPCKilled/AstralEnemyDeath");
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("MantisBanner").Type;
            SpawnModBiomes = new int[] { ModContent.GetInstance<Astral>().Type };
		}

        public override void AI()
        {
            NPC.TargetClosest(false);

            Player target = Main.player[NPC.target];

            if (NPC.ai[0] == 0f)
            {
                float acceleration = 0.045f;
                float maxSpeed = 6.8f;
                if (NPC.Center.X > target.Center.X)
                {
                    NPC.velocity.X -= acceleration;
                    if (NPC.velocity.X > 0)
                    {
                        NPC.velocity.X -= acceleration;
                    }
                    if (NPC.velocity.X < -maxSpeed)
                    {
                        NPC.velocity.X = -maxSpeed;
                    }
                }
                else
                {
                    NPC.velocity.X += acceleration;
                    if (NPC.velocity.X < 0)
                    {
                        NPC.velocity.X += acceleration;
                    }
                    if (NPC.velocity.X > maxSpeed)
                    {
                        NPC.velocity.X = maxSpeed;
                    }
                }

                //if need to jump
                if (NPC.velocity.Y == 0 && (HoleBelow() || (NPC.collideX && NPC.position.X == NPC.oldPosition.X)))
                {
                    NPC.velocity.Y = -5f;
                }

                //check if we can shoot at target.
                Vector2 vector = NPC.Center - target.Center;
                if (Math.Abs(vector.Y) < 64 && Math.Abs(vector.X) < 540 && Collision.CanHit(NPC.position, NPC.width, NPC.height, target.position, target.width, target.height))
                {
                    NPC.ai[1]++;
                    if (NPC.ai[1] > 120)
                    {
                        //fire projectile
                        NPC.ai[0] = 1f;
                        NPC.ai[1] = NPC.ai[2] = 0f;
                        NPC.frame.Y = 400; 
                        NPC.frameCounter = 0;
                    }
                }
                else
                {
                    NPC.ai[1] -= 0.5f;
                }

                if (NPC.justHit)
                {
                    NPC.ai[1] -= 60f;
                }


                if (NPC.ai[1] < 0) NPC.ai[1] = 0f;
            }
            else
            {
                NPC.ai[2]++;
                NPC.velocity.X *= 0.95f;
                if (NPC.ai[2] == 25f)
                {
                    SoundEngine.PlaySound(SoundID.Item71, NPC.position);
                    Vector2 vector = Main.player[NPC.target].Center - NPC.Center;
                    vector.Normalize();
                    Projectile.NewProjectile(NPC.GetSource_FromThis(null), NPC.Center + (NPC.Center.X < target.Center.X ? -14f : 14f) * Vector2.UnitX, vector * 7f, Mod.Find<ModProjectile>("MantisRing").Type, 76, 0.5f);
                }
            }

            NPC.direction = NPC.Center.X > target.Center.X ? 0 : 1;
            NPC.spriteDirection = NPC.direction;
        }

        private bool HoleBelow()
        {
            //width of npc in tiles
            int tileWidth = 4;
            int tileX = (int)(NPC.Center.X / 16f) - tileWidth;
            if (NPC.velocity.X > 0) //if moving right
            {
                tileX += tileWidth;
            }
            int tileY = (int)((NPC.position.Y + NPC.height) / 16f);
            for (int y = tileY; y < tileY + 2; y++)
            {
                for (int x = tileX; x < tileX + tileWidth; x++)
                {
                    if (Main.tile[x, y].HasTile)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override void FindFrame(int frameHeight)
        {
            if (NPC.ai[0] == 0f)
            {
                if (NPC.velocity.Y != 0)
                {
                    NPC.frame.Y = frameHeight * 13;
                    NPC.frameCounter = 20;
                }
                else
                {
                    NPC.frameCounter += 0.8f + Math.Abs(NPC.velocity.X) * 0.5f;
                    if (NPC.frameCounter > 10.0)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y += frameHeight;
                        if (NPC.frame.Y > frameHeight * 5)
                        {
                            NPC.frame.Y = 0;
                        }
                    }
                }
            }
            else
            {
                NPC.frameCounter++;
                if (NPC.frameCounter > 4)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y += frameHeight;
                    if (NPC.frame.Y >= frameHeight * 13)
                    {
                        NPC.frame.Y = 0;
                        NPC.frameCounter = 0;
                        NPC.ai[0] = 0f;
                    }
                }
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

            CalamityGlobalNPC.DoHitDust(NPC, hit.HitDirection, ModContent.DustType<AstralOrange>(), 1f, 4, 24);
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            //draw glowmask
            spriteBatch.Draw(glowmask, NPC.Center - Main.screenPosition - new Vector2(0, 8), NPC.frame, Color.White * 0.6f, NPC.rotation, new Vector2(70, 40), 1f, NPC.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneAstral && !spawnInfo.Player.ZoneRockLayerHeight)
            {
                return 0.16f;
            }
            return 0f;
        }
        
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("Stardust").Type, 1, 2, 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), Mod.Find<ModItem>("Stardust").Type, 1));
            npcLoot.Add(ItemDropRule.ByCondition(new DownedAstrumDeus(), Mod.Find<ModItem>("AstralScythe").Type, 7));
        }
    }
}
