using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;

namespace CalamityModClassicPreTrailer.NPCs.CosmicWraith
{
	public class SignusBomb : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cosmic Mine");
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Hide = true
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
		}
		
		public override void SetDefaults()
		{
			NPC.damage = 0;
			NPC.width = 30; //324
			NPC.height = 30; //216
			NPC.defense = 0;
			NPC.lifeMax = 100;
			NPC.aiStyle = -1; //new
            AIType = -1; //new
			NPC.knockBackResist = 0f;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit53;
			NPC.DeathSound = SoundID.NPCDeath44;
		}
		
		public override void AI()
		{
            NPC.rotation = NPC.velocity.X * 0.04f;
            if (NPC.ai[1] == 0f)
            {
                NPC.scale -= 0.02f;
                NPC.alpha += 30;
                if (NPC.alpha >= 250)
                {
                    NPC.alpha = 255;
                    NPC.ai[1] = 1f;
                }
            }
            else if (NPC.ai[1] == 1f)
            {
                NPC.scale += 0.02f;
                NPC.alpha -= 30;
                if (NPC.alpha <= 0)
                {
                    NPC.alpha = 0;
                    NPC.ai[1] = 0f;
                }
            }
            Player player = Main.player[NPC.target];
			if (!player.active || player.dead)
			{
				NPC.TargetClosest(false);
				player = Main.player[NPC.target];
				if (!player.active || player.dead)
				{
					if (NPC.timeLeft > 10)
					{
						NPC.timeLeft = 10;
					}
					return;
				}
			}
			else if (NPC.timeLeft > 600)
			{
				NPC.timeLeft = 600;
			}
            Vector2 vector = Main.player[NPC.target].Center - NPC.Center;
            if (vector.Length() < 90f || NPC.ai[3] >= 900f)
            {
                NPC.dontTakeDamage = false;
                CheckDead();
                NPC.life = 0;
                return;
            }
            NPC.ai[3] += 1f;
            NPC.dontTakeDamage = (NPC.ai[3] >= 750f ? false : true);
            if (NPC.ai[3] >= 600f)
            {
                NPC.velocity.Y *= 0.985f;
                NPC.velocity.X *= 0.985f;
                return;
            }
			NPC.TargetClosest(true);
			float num1372 = 14f;
			Vector2 vector167 = new Vector2(NPC.Center.X + (float)(NPC.direction * 20), NPC.Center.Y + 6f);
			float num1373 = player.position.X + (float)player.width * 0.5f - vector167.X;
			float num1374 = player.Center.Y - vector167.Y;
			float num1375 = (float)Math.Sqrt((double)(num1373 * num1373 + num1374 * num1374));
			float num1376 = num1372 / num1375;
			num1373 *= num1376;
			num1374 *= num1376;
			NPC.ai[0] -= 1f;
			if (num1375 < 200f || NPC.ai[0] > 0f)
			{
				if (num1375 < 200f)
				{
					NPC.ai[0] = 20f;
				}
				if (NPC.velocity.X < 0f)
				{
					NPC.direction = -1;
				}
				else
				{
					NPC.direction = 1;
				}
				return;
			}
			NPC.velocity.X = (NPC.velocity.X * 50f + num1373) / 51f;
			NPC.velocity.Y = (NPC.velocity.Y * 50f + num1374) / 51f;
			if (num1375 < 350f)
			{
				NPC.velocity.X = (NPC.velocity.X * 10f + num1373) / 11f;
				NPC.velocity.Y = (NPC.velocity.Y * 10f + num1374) / 11f;
			}
			if (num1375 < 300f)
			{
				NPC.velocity.X = (NPC.velocity.X * 7f + num1373) / 8f;
				NPC.velocity.Y = (NPC.velocity.Y * 7f + num1374) / 8f;
			}
			return;
		}

        public override Color? GetAlpha(Color drawColor)
        {
            return new Color(200, Main.DiscoG, 255, 0);
        }

        public override bool CheckDead()
        {
            SoundEngine.PlaySound(SoundID.Item14, NPC.position);
            NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
            NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
            NPC.damage = CalamityWorldPreTrailer.death ? 400 : 250;
            NPC.width = (NPC.height = 256);
            NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
            NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
            for (int num621 = 0; num621 < 15; num621++)
            {
                int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
                Main.dust[num622].velocity *= 3f;
                if (Main.rand.Next(2) == 0)
                {
                    Main.dust[num622].scale = 0.5f;
                    Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
                }
                Main.dust[num622].noGravity = true;
            }
            for (int num623 = 0; num623 < 30; num623++)
            {
                int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 173, 0f, 0f, 100, default(Color), 3f);
                Main.dust[num624].noGravity = true;
                Main.dust[num624].velocity *= 5f;
                num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
                Main.dust[num624].velocity *= 2f;
                Main.dust[num624].noGravity = true;
            }
            return true;
        }
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = 100;
			NPC.damage = 0;
		}
	}
}