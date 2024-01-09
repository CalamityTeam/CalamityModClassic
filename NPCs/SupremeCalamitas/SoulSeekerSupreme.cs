using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.NPCs.SupremeCalamitas
{
	public class SoulSeekerSupreme : ModNPC
	{
		public int timer = 0;
		public bool start = true;
		
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Soul Seeker");
		}
		
		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			AIType = -1;
			NPC.width = 40;
			NPC.height = 40;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.chaseable = false;
			NPC.damage = 300;
			NPC.defense = 250;
			NPC.lifeMax = 100000;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
				NPC.buffImmune[BuffID.Ichor] = false;
			}
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
                new FlavorTextBestiaryInfoElement("Orbital constructs that harvest the lifeforce of unfortunate victims.")

            });
        }

        public override bool PreAI()
		{
			bool expertMode = Main.expertMode;
			if (start)
			{
				for (int num621 = 0; num621 < 15; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.LifeDrain, 0f, 0f, 100, default(Color), 2f);
				}
				NPC.ai[1] = NPC.ai[0];
				start = false;
			}
			NPC.TargetClosest(true);
			Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
			direction.Normalize();
			direction *= 9f;
			NPC.rotation = direction.ToRotation();
			timer++;
			if (timer > 60)
			{
				if (Main.netMode != NetmodeID.MultiplayerClient && Main.rand.NextBool(4))
				{
					int damage = expertMode ? 100 : 112;
					int proj2 = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, direction.X, direction.Y, Mod.Find<ModProjectile>("BrimstoneBarrage").Type, damage, 1f, NPC.target);
				}
				timer = 0;
			}
			if (NPC.CountNPCS(Mod.Find<ModNPC>("SupremeCalamitas").Type) < 1)
			{
				NPC.active = false;
				return false;
			}
			Player player = Main.player[NPC.target];
			NPC parent = Main.npc[NPC.FindFirstNPC(Mod.Find<ModNPC>("SupremeCalamitas").Type)];
			double deg = (double)NPC.ai[1];
			double rad = deg * (Math.PI / 180);
			double dist = 1200;
			NPC.position.X = parent.Center.X - (int)(Math.Cos(rad) * dist) - NPC.width / 2;
			NPC.position.Y = parent.Center.Y - (int)(Math.Sin(rad) * dist) - NPC.height / 2;
			NPC.ai[1] += 2f;
			return false;
		}
		
		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			cooldownSlot = 1;
			return true;
		}
		
		public override void HitEffect(NPC.HitInfo hit)
		{
			if (NPC.life <= 0)
			{
				NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
				NPC.width = 50;
				NPC.height = 50;
				NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
				NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
				for (int num621 = 0; num621 < 5; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.LifeDrain, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.NextBool(2))
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 10; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.LifeDrain, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.LifeDrain, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}
		
		public override bool CheckActive()
		{
			return false;
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: balance -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.7f * balance);
			NPC.damage = (int)(NPC.damage * 0.7f);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			if (NPC.velocity != Vector2.Zero)
			{
				Texture2D texture = TextureAssets.Npc[NPC.type].Value;
				Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
				for (int i = 1; i < NPC.oldPos.Length; ++i)
				{
					Vector2 vector2_2 = NPC.oldPos[i];
					Microsoft.Xna.Framework.Color color2 = Color.White * NPC.Opacity;
					color2.R = (byte)(0.5 * (double)color2.R * (double)(10 - i) / 20.0);
					color2.G = (byte)(0.5 * (double)color2.G * (double)(10 - i) / 20.0);
					color2.B = (byte)(0.5 * (double)color2.B * (double)(10 - i) / 20.0);
					color2.A = (byte)(0.5 * (double)color2.A * (double)(10 - i) / 20.0);
					Main.spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, new Vector2(NPC.oldPos[i].X - Main.screenPosition.X + (NPC.width / 2),
						NPC.oldPos[i].Y - Main.screenPosition.Y + NPC.height / 2), new Rectangle?(NPC.frame), color2, NPC.oldRot[i], origin, NPC.scale, SpriteEffects.None, 0.0f);
				}
			}
			return true;
		}
	}
}