using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.NPCs.Polterghast
{
	public class PhantomFuckYou : ModNPC
	{
		public bool start = true;
        public int timer = 0;
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Phantom");
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
			NPC.width = 30;
			NPC.height = 30;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.damage = 0;
			NPC.defense = 0;
			NPC.lifeMax = 1500;
			NPC.dontTakeDamage = true;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
		}

		public override bool PreAI()
		{
			bool expertMode = (Main.expertMode || CalamityWorldPreTrailer.bossRushActive);
			if (start)
			{
				for (int num621 = 0; num621 < 5; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 180, 0f, 0f, 100, default(Color), 2f);
				}
				NPC.ai[1] = NPC.ai[0];
				start = false;
			}
            if (!Main.npc[CalamityGlobalNPC.ghostBoss].active)
            {
                NPC.active = false;
                NPC.netUpdate = true;
                return false;
            }
            NPC.TargetClosest(true);
			Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
			direction.Normalize();
			direction *= 9f;
			NPC.rotation = direction.ToRotation();
            timer++;
            if (CalamityWorldPreTrailer.death || CalamityWorldPreTrailer.bossRushActive)
            {
                timer++;
            }
            if (timer > 180)
            {
                if (Main.netMode != 1)
                {
                    int damage = expertMode ? 58 : 70;
                    Projectile.NewProjectile(NPC.GetSource_FromThis(null), NPC.Center.X, NPC.Center.Y, direction.X * 0.05f, direction.Y * 0.05f, Mod.Find<ModProjectile>("PhantomMine").Type, damage, 1f, NPC.target);
                }
                timer = 0;
            }
            Player player = Main.player[NPC.target];
			double deg = (double)NPC.ai[1];
			double rad = deg * (Math.PI / 180);
			double dist = 500;
			NPC.position.X = player.Center.X - (int)(Math.Cos(rad) * dist) - NPC.width / 2;
			NPC.position.Y = player.Center.Y - (int)(Math.Sin(rad) * dist) - NPC.height / 2;
			NPC.ai[1] += 0.5f; //1f
			return false;
		}
		
		public override Color? GetAlpha(Color drawColor)
		{
			return new Color(200, 200, 200, NPC.alpha);
		}
		
		public override bool CheckActive()
		{
			return false;
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.5f * balance);
			NPC.damage = (int)(NPC.damage * 0.5f);
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