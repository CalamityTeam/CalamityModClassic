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
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.NPCs.AstralBiomeNPCs
{
    public class Aries : ModNPC
    {
        private static Texture2D glowmask;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Aries");
            Main.npcFrameCount[NPC.type] = 8;
            if (!Main.dedServ)
                glowmask = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AstralBiomeNPCs/AriesGlow").Value;
        }
        
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                new FlavorTextBestiaryInfoElement("These creatures hop and chase any threat to the infection relentlessly.")
            });
        }

        public override void SetDefaults()
        {
            NPC.damage = 85;
            NPC.width = 56;
            NPC.height = 54;
            NPC.aiStyle = 41;
            NPC.defense = 24;
            NPC.lifeMax = 450;
            NPC.knockBackResist = 0.5f;
            NPC.value = Item.buyPrice(0, 0, 10, 0);
            NPC.DeathSound = new SoundStyle("CalamityModClassicPreTrailer/Sounds/NPCKilled/AstralEnemyDeath");
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("AriesBanner").Type;
            SpawnModBiomes = new int[] { ModContent.GetInstance<Astral>().Type };
		}

        public override void FindFrame(int frameHeight)
        {
            CalamityGlobalNPC.SpawnDustOnNPC(NPC, 66, frameHeight, ModContent.DustType<AstralOrange>(), new Rectangle(44, 18, 12, 12));
            if (NPC.velocity.Y == 0f)
            {
                NPC.frame.Y = 0;
            }
            else if ((double)NPC.velocity.Y < -1.5)
            {
                NPC.frame.Y = frameHeight * 7;
            }
            else if ((double)NPC.velocity.Y < 0)
            {
                NPC.frame.Y = frameHeight * 4;
            }
            else if ((double)NPC.velocity.Y > 1.5)
            {
                NPC.frame.Y = frameHeight * 6;
            }
            else
            {
                NPC.frame.Y = frameHeight * 5;
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
            spriteBatch.Draw(glowmask, NPC.Center - Main.screenPosition, NPC.frame, Color.White * 0.6f, NPC.rotation, new Vector2(33, 31), 1f, NPC.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.GetModPlayer<CalamityPlayerPreTrailer>().ZoneAstral && !spawnInfo.Player.ZoneRockLayerHeight)
            {
                return 0.15f;
            }
            return 0f;
        }
        
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("Stardust").Type, 2, 1, 3));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), Mod.Find<ModItem>("Stardust").Type, 1));
            npcLoot.Add(ItemDropRule.ByCondition(new DownedAstrumDeus(), Mod.Find<ModItem>("StellarKnife").Type, 7));
        }
    }
}
