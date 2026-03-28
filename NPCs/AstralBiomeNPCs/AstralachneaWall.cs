using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalamityModClassicPreTrailer.Dusts;
using CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.AstralBiomeNPCs
{
    public class AstralachneaWall : ModNPC
    {
        private static Texture2D glowmask;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Astralachnea");

            Main.npcFrameCount[NPC.type] = 4;

            if (!Main.dedServ)
                glowmask = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/NPCs/AstralBiomeNPCs/AstralachneaWallGlow").Value;

            base.SetStaticDefaults();
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 60;
            NPC.height = 60;
            NPC.aiStyle = -1;
            NPC.damage = 90;
            NPC.defense = 30;
            NPC.lifeMax = 750;
            NPC.DeathSound = new SoundStyle("CalamityModClassicPreTrailer/Sounds/NPCKilled/AstralEnemyDeath");
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.value = Item.buyPrice(0, 0, 20, 0);
            NPC.buffImmune[20] = true;
            NPC.buffImmune[31] = false;
            NPC.timeLeft = NPC.activeTime * 2;
            AnimationType = NPCID.BlackRecluseWall;
			Banner = Mod.Find<ModNPC>("AstralachneaGround").Type;
			BannerItem = Mod.Find<ModItem>("AstralachneaBanner").Type;
		}

        public override void AI()
        {
            CalamityGlobalNPC.DoSpiderWallAI(NPC, Mod.Find<ModNPC>("AstralachneaGround").Type, 2.4f, 0.1f);
        }

        public override void FindFrame(int frameHeight)
        {
            //DO DUST
            int frame = NPC.frame.Y / frameHeight;
            Rectangle rect = new Rectangle(12, 24, 18, 10);
            Rectangle rect2 = new Rectangle(12, 44, 18, 10);
            switch (frame)
            {
                case 1:
                    rect = new Rectangle(6, 26, 28, 8);
                    rect2 = new Rectangle(6, 44, 28, 8);
                    break;
                case 2:
                    rect = new Rectangle(12, 26, 18, 8);
                    rect2 = new Rectangle(12, 44, 18, 8);
                    break;
                case 3:
                    rect = new Rectangle(16, 24, 16, 10);
                    rect2 = new Rectangle(16, 44, 16, 10);
                    break;
            }
            Dust d = CalamityGlobalNPC.SpawnDustOnNPC(NPC, 80, frameHeight, ModContent.DustType<AstralOrange>(), rect, Vector2.Zero, 0.225f, true);
            Dust d2 = CalamityGlobalNPC.SpawnDustOnNPC(NPC, 80, frameHeight, ModContent.DustType<AstralOrange>(), rect2, Vector2.Zero, 0.225f, true);
            if (d != null)
            {
                d.customData = 0.04f;
            }
            if (d2 != null)
            {
                d2.customData = 0.04f;
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

            CalamityGlobalNPC.DoHitDust(NPC, hit.HitDirection, (Main.rand.Next(0, Math.Max(0, NPC.life)) == 0) ? 5 : ModContent.DustType<AstralEnemy>(), 1f, 4, 22);

            //if dead do gores
            if (NPC.life <= 0)
            {
                if (Main.netMode != NetmodeID.Server)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        Gore.NewGore(NPC.GetSource_FromThis(null), NPC.Center, NPC.velocity * 0.3f,
                            Mod.Find<ModGore>("AstralachneaGore" + i).Type);
                    }
                }
            }
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Vector2 origin = new Vector2(40f, 40f);
            spriteBatch.Draw(glowmask, NPC.Center - Main.screenPosition - new Vector2(0, 8f), NPC.frame, Color.White * 0.6f, NPC.rotation, origin, 1f, SpriteEffects.None, 0);
        }
        
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(new CommonDrop(Mod.Find<ModItem>("Stardust").Type, 1, 2, 4));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), Mod.Find<ModItem>("Stardust").Type, 1));
            npcLoot.Add(ItemDropRule.ByCondition(new DownedAstrumDeus(), Mod.Find<ModItem>("AstralachneaStaff").Type, 7));
        }
    }
}
