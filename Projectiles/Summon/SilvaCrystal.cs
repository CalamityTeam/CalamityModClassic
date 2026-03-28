using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Summon
{
    public class SilvaCrystal : ModProjectile
    {
    	public int dust = 3;
    	
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Silva Crystal");
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.ignoreWater = true;
			Projectile.minion = true;
			Projectile.minionSlots = 0f;
            Projectile.timeLeft = 18000;
            Projectile.alpha = 255;
            Projectile.tileCollide = false;
            Projectile.timeLeft *= 5;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
			if (Projectile.localAI[1] == 0f)
			{
				Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionDamageValue = Main.player[Projectile.owner].GetDamage(DamageClass.Summon).Base;
				Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionProjectileDamageValue = Projectile.damage;
				Projectile.localAI[1] += 1f;
			}
			if (Main.player[Projectile.owner].GetDamage(DamageClass.Summon).Base != Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionDamageValue)
			{
				int damage2 = (int)(((float)Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionProjectileDamageValue /
					Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionDamageValue) *
					Main.player[Projectile.owner].GetDamage(DamageClass.Summon).Base);
				Projectile.damage = damage2;
			}
			bool flag64 = Projectile.type == Mod.Find<ModProjectile>("SilvaCrystal").Type;
            Player player = Main.player[Projectile.owner];
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            if (!modPlayer.silvaSummon)
            {
                Projectile.active = false;
                return;
            }
            if (flag64)
            {
                if (player.dead)
                {
                    modPlayer.sCrystal = false;
                }
                if (modPlayer.sCrystal)
                {
                    Projectile.timeLeft = 2;
                }
            }
            Projectile.position.X = Main.player[Projectile.owner].Center.X - (float)(Projectile.width / 2);
            Projectile.position.Y = Main.player[Projectile.owner].Center.Y - (float)(Projectile.height / 2) + Main.player[Projectile.owner].gfxOffY - 60f;
            if (Main.player[Projectile.owner].gravDir == -1f)
            {
                Projectile.position.Y = Projectile.position.Y + 120f;
                Projectile.rotation = 3.14f;
            }
            else
            {
                Projectile.rotation = 0f;
            }
            Projectile.position.X = (float)((int)Projectile.position.X);
            Projectile.position.Y = (float)((int)Projectile.position.Y);
            float num1072 = 1500f;
            Projectile.velocity = Vector2.Zero;
            Projectile.alpha -= 5;
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
            if (Projectile.direction == 0)
            {
                Projectile.direction = Main.player[Projectile.owner].direction;
            }
            if (Projectile.alpha == 0 && Main.rand.Next(15) == 0)
            {
                Dust dust34 = Main.dust[Dust.NewDust(Projectile.Top, 0, 0, 267, 0f, 0f, 100, new Color(Main.DiscoR, 203, 103), 1f)];
                dust34.velocity.X = 0f;
                dust34.noGravity = true;
                dust34.fadeIn = 1f;
                dust34.position = Projectile.Center + Vector2.UnitY.RotatedByRandom(6.2831854820251465) * (4f * Main.rand.NextFloat() + 26f);
                dust34.scale = 0.5f;
            }
            float[] var_2_31123_cp_0 = Projectile.localAI;
            int var_2_31123_cp_1 = 0;
            float num73 = var_2_31123_cp_0[var_2_31123_cp_1];
            var_2_31123_cp_0[var_2_31123_cp_1] = num73 + 1f;
            if (Projectile.localAI[0] >= 60f)
            {
                Projectile.localAI[0] = 0f;
            }
            if (Projectile.ai[0] < 0f)
            {
                float[] var_2_31169_cp_0 = Projectile.ai;
                int var_2_31169_cp_1 = 0;
                float num730 = var_2_31169_cp_0[var_2_31169_cp_1];
                var_2_31169_cp_0[var_2_31169_cp_1] = num730 + 1f;
            }
            if (Projectile.ai[0] == 0f)
            {
                int num1074 = -1;
                float num1075 = num1072;
				if (player.HasMinionAttackTargetNPC)
				{
					NPC npc = Main.npc[player.MinionAttackTargetNPC];
					if (npc.CanBeChasedBy(Projectile, false))
					{
						float num1076 = Projectile.Distance(npc.Center);
						if (num1076 < num1075 && Collision.CanHitLine(Projectile.Center, 0, 0, npc.Center, 0, 0))
						{
							num1075 = num1076;
							num1074 = npc.whoAmI;
						}
					}
				}
                if (num1074 < 0)
                {
                    int num30;
                    for (int num1077 = 0; num1077 < 200; num1077 = num30 + 1)
                    {
                        NPC nPC16 = Main.npc[num1077];
                        if (nPC16.CanBeChasedBy(Projectile, false))
                        {
                            float num1078 = Projectile.Distance(nPC16.Center);
                            if (num1078 < num1075 && Collision.CanHitLine(Projectile.Center, 0, 0, nPC16.Center, 0, 0))
                            {
                                num1075 = num1078;
                                num1074 = num1077;
                            }
                        }
                        num30 = num1077;
                    }
                }
                if (num1074 != -1)
                {
                    Projectile.ai[0] = 1f;
                    Projectile.ai[1] = (float)num1074;
                    Projectile.netUpdate = true;
                    return;
                }
            }
            if (Projectile.ai[0] > 0f)
            {
                int num1079 = (int)Projectile.ai[1];
                if (!Main.npc[num1079].CanBeChasedBy(Projectile, false))
                {
                    Projectile.ai[0] = 0f;
                    Projectile.ai[1] = 0f;
                    Projectile.netUpdate = true;
                    return;
                }
                float[] var_2_31390_cp_0 = Projectile.ai;
                int var_2_31390_cp_1 = 0;
                float num731 = var_2_31390_cp_0[var_2_31390_cp_1];
                var_2_31390_cp_0[var_2_31390_cp_1] = num731 + 1f;
                if (Projectile.ai[0] >= 5f)
                {
                    Vector2 vector154 = Projectile.DirectionTo(Main.npc[num1079].Center);
                    if (vector154.HasNaNs())
                    {
                        vector154 = Vector2.UnitY;
                    }
                    int num1082 = (vector154.X > 0f) ? 1 : -1;
                    Projectile.direction = num1082;
                    Projectile.ai[0] = -20f;
                    Projectile.netUpdate = true;
                    if (Projectile.owner == Main.myPlayer)
                    {
                        Vector2 vector155 = Main.npc[num1079].position + Main.npc[num1079].Size * Utils.RandomVector2(Main.rand, 0f, 1f) - Projectile.Center;
                        int num31;
                        for (int num1083 = 0; num1083 < 3; num1083 = num31 + 1)
                        {
                            Vector2 vector156 = Projectile.Center + vector155;
                            if (num1083 > 0)
                            {
                                vector156 = Projectile.Center + vector155.RotatedByRandom(0.78539818525314331) * (Main.rand.NextFloat() * 0.5f + 0.75f);
                            }
                            float x4 = Main.rgbToHsl(new Color(Main.DiscoR, 203, 103)).X;
                            Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector156.X, vector156.Y, 0f, 0f, Mod.Find<ModProjectile>("SilvaCrystalExplosion").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, x4, (float)Projectile.whoAmI);
                            num31 = num1083;
                        }
                        return;
                    }
                }
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 127 - Projectile.alpha / 2);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Microsoft.Xna.Framework.Color color25 = Lighting.GetColor((int)((double)Projectile.position.X + (double)Projectile.width * 0.5) / 16, (int)(((double)Projectile.position.Y + (double)Projectile.height * 0.5) / 16.0));
            Vector2 vector59 = Projectile.position + new Vector2((float)Projectile.width, (float)Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
            Texture2D texture2D34 = TextureAssets.Projectile[Projectile.type].Value;
            Microsoft.Xna.Framework.Rectangle rectangle17 = texture2D34.Frame(1, Main.projFrames[Projectile.type], 0, Projectile.frame);
            Microsoft.Xna.Framework.Color alpha5 = Projectile.GetAlpha(color25);
            Vector2 origin11 = rectangle17.Size() / 2f;
            float scaleFactor5 = (float)Math.Cos((double)(6.28318548f * (Projectile.localAI[0] / 60f))) + 3f + 3f;
            for (float num286 = 0f; num286 < 4f; num286 += 1f)
            {
                SpriteBatch arg_F907_0 = Main.spriteBatch;
                Texture2D arg_F907_1 = texture2D34;
                Vector2 arg_F8CE_0 = vector59;
                Vector2 arg_F8BE_0 = Vector2.UnitY;
                double arg_F8BE_1 = (double)(num286 * 1.57079637f);
                Vector2 center = default(Vector2);
                arg_F907_0.Draw(arg_F907_1, arg_F8CE_0 + arg_F8BE_0.RotatedBy(arg_F8BE_1, center) * scaleFactor5, new Microsoft.Xna.Framework.Rectangle?(rectangle17), alpha5 * 0.2f, Projectile.rotation, origin11, Projectile.scale, SpriteEffects.None, 0f);
            }
            return false;
        }

		public override bool? CanDamage()/* tModPorter Suggestion: Return null instead of true */
		{
			return false;
		}
	}
}