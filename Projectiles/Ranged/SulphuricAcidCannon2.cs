using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Ranged
{
    public class SulphuricAcidCannon2 : ModProjectile
    {
        public float counter = 0f;
        public float counter2 = 0f;
        public int killCounter = 0;

    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Acid Bubble");
            Main.projFrames[Projectile.type] = 7;
        }
    	
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
        }

        public override void AI()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 6)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame > 6)
            {
                Projectile.frame = 0;
            }
            if (Projectile.owner == Main.myPlayer)
            {
                if (counter >= 90f)
                {
                    counter = 0f;
                    int num320 = Main.rand.Next(1, 3);
                    int num3;
                    for (int num321 = 0; num321 < num320; num321 = num3 + 1)
                    {
                        Vector2 vector15 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                        vector15.Normalize();
                        vector15 *= (float)Main.rand.Next(50, 401) * 0.01f;
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, vector15.X, vector15.Y, Mod.Find<ModProjectile>("SulphuricAcidMist2").Type, (int)(210f * Main.player[Projectile.owner].GetDamage(DamageClass.Ranged).Base), 1f, Projectile.owner, 0f, 0f);
                        num3 = num321;
                    }
                }
                else
                {
                    counter += 1f;
                }
            }
            if (Projectile.ai[0] == 0f)
            {
                float[] var_2_2CA48_cp_0 = Projectile.ai;
                int var_2_2CA48_cp_1 = 1;
                float num73 = var_2_2CA48_cp_0[var_2_2CA48_cp_1];
                var_2_2CA48_cp_0[var_2_2CA48_cp_1] = num73 + 1f;
                if (Projectile.ai[1] >= 6f)
                {
                    int num982 = 20;
                    if (Projectile.alpha > 0)
                    {
                        Projectile.alpha -= num982;
                    }
                    if (Projectile.alpha < 80)
                    {
                        Projectile.alpha = 80;
                    }
                }
                if (Projectile.ai[1] >= 45f)
                {
                    Projectile.ai[1] = 45f;
                    if (counter2 < 1f)
                    {
                        counter2 += 0.002f;
                        Projectile.scale += 0.002f;
                        Projectile.width = (int)(30f * Projectile.scale);
                        Projectile.height = (int)(30f * Projectile.scale);
                    }
                    else
                    {
                        Projectile.width = 60;
                        Projectile.height = 60;
                    }
                    if (Projectile.wet)
                    {
                        if (Projectile.velocity.Y > 0f)
                        {
                            Projectile.velocity.Y = Projectile.velocity.Y * 0.98f;
                        }
                        if (Projectile.velocity.Y > -1f)
                        {
                            Projectile.velocity.Y = Projectile.velocity.Y - 0.2f;
                        }
                    }
                    else if (Projectile.velocity.Y > -2f)
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y - 0.05f;
                    }
                }
                killCounter++;
                if (killCounter >= 200)
                {
                    Projectile.Kill();
                }
            }
            if (Projectile.ai[0] == 1f)
            {
                Projectile.tileCollide = false;
                int num988 = 15;
                bool flag54 = false;
                bool flag55 = false;
                float[] var_2_2CB4E_cp_0 = Projectile.localAI;
                int var_2_2CB4E_cp_1 = 0;
                float num73 = var_2_2CB4E_cp_0[var_2_2CB4E_cp_1];
                var_2_2CB4E_cp_0[var_2_2CB4E_cp_1] = num73 + 1f;
                if (Projectile.localAI[0] % 30f == 0f)
                {
                    flag55 = true;
                }
                int num989 = (int)Projectile.ai[1];
                if (Projectile.localAI[0] >= (float)(60 * num988))
                {
                    flag54 = true;
                }
                else if (num989 < 0 || num989 >= 200)
                {
                    flag54 = true;
                }
                else if (Main.npc[num989].active && !Main.npc[num989].dontTakeDamage)
                {
                    Projectile.Center = Main.npc[num989].Center - Projectile.velocity * 2f;
                    Projectile.gfxOffY = Main.npc[num989].gfxOffY;
                    if (flag55)
                    {
                        Main.npc[num989].HitEffect(0, 1.0);
                    }
                }
                else
                {
                    flag54 = true;
                }
                if (flag54)
                {
                    Projectile.Kill();
                }
            }
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            Rectangle myRect = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height);
            if (Projectile.owner == Main.myPlayer)
            {
                for (int i = 0; i < 200; i++)
                {
                    if (((Main.npc[i].active && !Main.npc[i].dontTakeDamage)) &&
                        ((Projectile.friendly && (!Main.npc[i].friendly || Projectile.type == 318 || (Main.npc[i].type == 22 && Projectile.owner < 255 && Main.player[Projectile.owner].killGuide) || (Main.npc[i].type == 54 && Projectile.owner < 255 && Main.player[Projectile.owner].killClothier))) ||
                        (Projectile.hostile && Main.npc[i].friendly && !Main.npc[i].dontTakeDamageFromHostiles)) && (Projectile.owner < 0 || Main.npc[i].immune[Projectile.owner] == 0 || Projectile.maxPenetrate == 1))
                    {
                        if (Main.npc[i].noTileCollide || !Projectile.ownerHitCheck)
                        {
                            bool flag3;
                            if (Main.npc[i].type == 414)
                            {
                                Rectangle rect = Main.npc[i].getRect();
                                int num5 = 8;
                                rect.X -= num5;
                                rect.Y -= num5;
                                rect.Width += num5 * 2;
                                rect.Height += num5 * 2;
                                flag3 = Projectile.Colliding(myRect, rect);
                            }
                            else
                            {
                                flag3 = Projectile.Colliding(myRect, Main.npc[i].getRect());
                            }
                            if (flag3)
                            {
                                if (Main.npc[i].reflectsProjectiles && Projectile.CanBeReflected())
                                {
                                    Main.npc[i].ReflectProjectile(Projectile);
                                    return;
                                }
                                Projectile.ai[0] = 1f;
                                Projectile.ai[1] = (float)i;
                                Projectile.velocity = (Main.npc[i].Center - Projectile.Center) * 0.75f;
                                Projectile.netUpdate = true;
                                Projectile.StatusNPC(i);
                                Projectile.damage = 0;
                                int num28 = 4;
                                Point[] array2 = new Point[num28];
                                int num29 = 0;
                                for (int l = 0; l < 1000; l++)
                                {
                                    if (l != Projectile.whoAmI && Main.projectile[l].active && Main.projectile[l].owner == Main.myPlayer && Main.projectile[l].type == Projectile.type && Main.projectile[l].ai[0] == 1f && Main.projectile[l].ai[1] == (float)i)
                                    {
                                        array2[num29++] = new Point(l, Main.projectile[l].timeLeft);
                                        if (num29 >= array2.Length)
                                        {
                                            break;
                                        }
                                    }
                                }
                                if (num29 >= array2.Length)
                                {
                                    int num30 = 0;
                                    for (int m = 1; m < array2.Length; m++)
                                    {
                                        if (array2[m].Y < array2[num30].Y)
                                        {
                                            num30 = m;
                                        }
                                    }
                                    Main.projectile[array2[num30].X].Kill();
                                }
                            }
                        }
                    }
                }
            }
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
            {
                targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
            }
            return null;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture2D13 = TextureAssets.Projectile[Projectile.type].Value;
            int num214 = TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
            int y6 = num214 * Projectile.frame;
            Main.spriteBatch.Draw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y6, texture2D13.Width, num214)), Projectile.GetAlpha(lightColor), Projectile.rotation, new Vector2((float)texture2D13.Width / 2f, (float)num214 / 2f), Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.buffImmune[BuffID.Venom] && target.aiStyle != 6)
            {
                target.buffImmune[BuffID.Venom] = false;
            }
            target.AddBuff(BuffID.Venom, 600);
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item54, Projectile.position);
            Projectile.position = Projectile.Center;
            Projectile.width = (Projectile.height = 60);
            Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
            Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
            int num3;
            for (int num246 = 0; num246 < 25; num246 = num3 + 1)
            {
                int num247 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 31, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num247].position = (Main.dust[num247].position + Projectile.position) / 2f;
                Main.dust[num247].velocity = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                Main.dust[num247].velocity.Normalize();
                Dust dust = Main.dust[num247];
                dust.velocity *= (float)Main.rand.Next(1, 30) * 0.1f;
                Main.dust[num247].alpha = Projectile.alpha;
                num3 = num246;
            }
        }
    }
}