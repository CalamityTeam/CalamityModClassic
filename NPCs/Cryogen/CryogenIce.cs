using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;

namespace CalamityModClassicPreTrailer.NPCs.Cryogen
{
	public class CryogenIce : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cryogen's Shield");
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Hide = true
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.canGhostHeal = false;
			NPC.noTileCollide = true;
            NPC.damage = 25;
			NPC.width = 240; //324
			NPC.height = 240; //216
			NPC.defense = 0;
			NPC.lifeMax = 1400;
            if (CalamityWorldPreTrailer.bossRushActive)
            {
                NPC.lifeMax = 100000;
            }
            NPC.alpha = 255;
			NPC.HitSound = SoundID.NPCHit5;
			NPC.DeathSound = SoundID.NPCDeath7;
		}
		
		public override void AI()
		{
			NPC.alpha -= 3;
			if (NPC.alpha < 0)
			{
				NPC.alpha = 0;
			}
			NPC.rotation += 0.15f;
			if (NPC.type == Mod.Find<ModNPC>("CryogenIce").Type)
			{
				int num989 = (int)NPC.ai[0];
				if (Main.npc[num989].active && Main.npc[num989].type == Mod.Find<ModNPC>("Cryogen").Type)
				{
					NPC.velocity = Vector2.Zero;
					NPC.position = Main.npc[num989].Center;
					NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
					NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
					NPC.gfxOffY = Main.npc[num989].gfxOffY;
					return;
				}
				NPC.life = 0;
				NPC.HitEffect(0, 10.0);
				NPC.active = false;
			}
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return NPC.alpha == 0;
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffID.Frostburn, 90, true);
            target.AddBuff(BuffID.Chilled, 60, true);
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.5f * balance);
			NPC.damage *= 2;
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 3; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 67, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int num621 = 0; num621 < 25; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 67, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 50; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 67, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 67, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
                Vector2 value9 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                float spread = 45f * 0.0174f;
                double startAngle = Math.Atan2(NPC.velocity.X, NPC.velocity.Y) - spread / 2;
                double deltaAngle = spread / 4f;
                double offsetAngle;
                int num184 = Main.expertMode ? 20 : 23;
                int i;
                for (i = 0; i < 2; i++)
                {
                    offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
                    int ice = Projectile.NewProjectile(Entity.GetSource_FromThis(null), value9.X, value9.Y, (float)(Math.Sin(offsetAngle) * 8f), (float)(Math.Cos(offsetAngle) * 8f), Mod.Find<ModProjectile>("IceBlast").Type, num184, 0f, Main.myPlayer, 0f, 0f);
                    int ice2 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), value9.X, value9.Y, (float)(-Math.Sin(offsetAngle) * 8f), (float)(-Math.Cos(offsetAngle) * 8f), Mod.Find<ModProjectile>("IceBlast").Type, num184, 0f, Main.myPlayer, 0f, 0f);
                    Main.projectile[ice].timeLeft = 300;
                    Main.projectile[ice2].timeLeft = 300;
                }
                float randomSpread = (float)(Main.rand.Next(-200, 200) / 100);
                if (Main.netMode != NetmodeID.Server)
                {
	                for (int spike = 0; spike < 4; spike++)
	                {
		                randomSpread = (float)(Main.rand.Next(-200, 200) / 100);
		                Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity * randomSpread, Mod.Find<ModGore>("CryoGore4").Type, 1f);
		                Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity * randomSpread, Mod.Find<ModGore>("CryoGore5").Type, 1f);
		                Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity * randomSpread, Mod.Find<ModGore>("CryoGore6").Type, 1f);
		                Gore.NewGore(NPC.GetSource_FromThis(null), NPC.position, NPC.velocity * randomSpread, Mod.Find<ModGore>("CryoGore7").Type, 1f);
	                }
                }
            }
		}
	}
}