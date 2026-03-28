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
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.NPCs.AstralBiomeNPCs
{
    public class Hadarian : ModNPC
    {
        private static Texture2D glowmask;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hadarian");
            if (!Main.dedServ)
                glowmask = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AstralBiomeNPCs/HadarianGlow").Value;
            Main.npcFrameCount[NPC.type] = 7;
        }
        
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                new FlavorTextBestiaryInfoElement("These creatures possess extremely tough membranes in their wings, allowing them to apply pressure on their enemies even if there's counterattack.")
            });
        }

        public override void SetDefaults()
        {
            NPC.width = 50;
            NPC.height = 40;
            NPC.aiStyle = -1;
            NPC.damage = 78;
            NPC.defense = 18;
            NPC.lifeMax = 490;
            NPC.DeathSound = new SoundStyle("CalamityModClassicPreTrailer/Sounds/NPCKilled/AstralEnemyDeath");
            NPC.knockBackResist = 0.65f;
            NPC.value = Item.buyPrice(0, 0, 15, 0);
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("HadarianBanner").Type;
            SpawnModBiomes = new int[] { ModContent.GetInstance<Astral>().Type };
		}

        public override void AI()
        {
            CalamityGlobalNPC.DoVultureAI(NPC, 0.15f, 3.5f, 32, 50, 150, 150);

            //usually done in framing but I put it here because it makes more sense to.
            NPC.rotation = NPC.velocity.X * 0.1f;
        }

        public override void FindFrame(int frameHeight)
        {
            if (NPC.velocity.Y == 0f)
            {
                NPC.spriteDirection = NPC.direction;
            }
            else
            {
                if ((double)NPC.velocity.X > 0.5)
                {
                    NPC.spriteDirection = 1;
                }
                if ((double)NPC.velocity.X < -0.5)
                {
                    NPC.spriteDirection = -1;
                }
            }

            if (NPC.velocity.X == 0f && NPC.velocity.Y == 0f)
            {
                NPC.frame.Y = 0;
                NPC.frameCounter = 0.0;
            }
            else
            {
                NPC.frameCounter++;
                if (NPC.frameCounter > 5)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y += frameHeight;
                }
                if (NPC.frame.Y > frameHeight * 6 || NPC.frame.Y == 0)
                {
                    NPC.frame.Y = frameHeight;
                }
            }

            DoWingDust(frameHeight);
        }

        private void DoWingDust(int frameHeight)
        {
            int frame = NPC.frame.Y / frameHeight;
            Dust d = null;
            switch (frame)
            {
                case 1:
                    d = CalamityGlobalNPC.SpawnDustOnNPC(NPC, 82, frameHeight, ModContent.DustType<AstralOrange>(), new Rectangle(38, 16, 22, 20), Vector2.Zero, 0.35f);
                    break;
                case 2:
                    d = CalamityGlobalNPC.SpawnDustOnNPC(NPC, 82, frameHeight, ModContent.DustType<AstralOrange>(), new Rectangle(38, 24, 30, 14), Vector2.Zero);
                    break;
                case 3:
                    d = CalamityGlobalNPC.SpawnDustOnNPC(NPC, 82, frameHeight, ModContent.DustType<AstralOrange>(), new Rectangle(44, 28, 32, 20), Vector2.Zero);
                    break;
                case 4:
                    d = CalamityGlobalNPC.SpawnDustOnNPC(NPC, 82, frameHeight, ModContent.DustType<AstralOrange>(), new Rectangle(42, 36, 18, 30), Vector2.Zero, 0.3f);
                    break;
            }

            if (d != null)
            {
                d.customData = 0.03f;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (!NPC.active || NPC.IsABestiaryIconDummy)
            {
                return true;
            }
            if (NPC.ai[0] == 0f)
            {
                Vector2 position = NPC.Bottom - new Vector2(19f, 42f);
                //20 34 38 42
                Rectangle src = new Rectangle(20, 34, 38, 42);
                spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, position - Main.screenPosition, src, drawColor, NPC.rotation, default(Vector2), 1f, NPC.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
                //draw glowmask
                spriteBatch.Draw(glowmask, position - Main.screenPosition, src, Color.White * 0.6f, NPC.rotation, default(Vector2), 1f, NPC.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
                return false;
            }
            return true;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (NPC.ai[0] != 0f)
            {
                Vector2 origin = new Vector2(41f, 39f);

                //draw glowmask
                spriteBatch.Draw(glowmask, NPC.Center - Main.screenPosition - new Vector2(0f, 12f), NPC.frame, Color.White * 0.6f, NPC.rotation, origin, 1f, NPC.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
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

            CalamityGlobalNPC.DoHitDust(NPC, hit.HitDirection, (Main.rand.Next(0, Math.Max(0, NPC.life)) == 0) ? 5 : ModContent.DustType<AstralEnemy>(), 1f, 3, 20);

            //if dead do gores
            if (NPC.life <= 0)
            {
                if (Main.netMode != NetmodeID.Server)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Gore.NewGore(NPC.GetSource_FromThis(null), NPC.Center, NPC.velocity * 0.3f,
                            Mod.Find<ModGore>("HadarianGore" + i).Type);
                    }
                }
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Tile tile = Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY];
            if (spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneAstral && spawnInfo.Player.ZoneDesert && spawnInfo.SpawnTileType == Mod.Find<ModTile>("AstralSand").Type && tile.WallType == WallID.None)
            {
                return 0.25f;
            }
            return 0f;
        }
        
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("Stardust").Type, 1, 2, 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), Mod.Find<ModItem>("Stardust").Type, 1));
            npcLoot.Add(ItemDropRule.ByCondition(new DownedAstrumDeus(), Mod.Find<ModItem>("HadarianMembrane").Type, 2, 1, 3));
        }
    }
}
