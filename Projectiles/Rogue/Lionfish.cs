using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Rogue
{
    public class Lionfish : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Lionfish");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
			Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().rogue = true;
		}
        
        public override void AI()
        {
            int num982 = 25;
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= num982;
            }
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
            if (Projectile.ai[0] == 0f)
            {
                float[] var_2_2CA48_cp_0 = Projectile.ai;
                int var_2_2CA48_cp_1 = 1;
                float num73 = var_2_2CA48_cp_0[var_2_2CA48_cp_1];
                var_2_2CA48_cp_0[var_2_2CA48_cp_1] = num73 + 1f;
                if (Projectile.ai[1] >= 45f)
                {
                    float num986 = 0.98f;
                    float num987 = 0.35f;
                    Projectile.ai[1] = 45f;
                    Projectile.velocity.X = Projectile.velocity.X * num986;
                    Projectile.velocity.Y = Projectile.velocity.Y + num987;
                    if (Projectile.velocity.X < 0f)
                    {
                        Projectile.spriteDirection = -1;
                        Projectile.rotation = (float)Math.Atan2((double)(-(double)Projectile.velocity.Y), (double)(-(double)Projectile.velocity.X));
                    }
                    else
                    {
                        Projectile.spriteDirection = 1;
                        Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X);
                    }
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
                                int num28 = 6;
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
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, Projectile.GetAlpha(lightColor), Projectile.rotation, tex.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
        
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            Projectile.position = Projectile.Center;
            Projectile.width = (Projectile.height = 72);
            Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
            Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
            for (int num193 = 0; num193 < 3; num193++)
            {
                Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 14, 0f, 0f, 100, new Color(0, 255, 255), 1.5f);
            }
            for (int num194 = 0; num194 < 30; num194++)
            {
                int num195 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 14, 0f, 0f, 0, new Color(0, 255, 255), 2.5f);
                Main.dust[num195].noGravity = true;
                Main.dust[num195].velocity *= 3f;
                num195 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 14, 0f, 0f, 100, new Color(0, 255, 255), 1.5f);
                Main.dust[num195].velocity *= 2f;
                Main.dust[num195].noGravity = true;
            }
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.type == NPCID.KingSlime || target.type == NPCID.WallofFlesh || target.type == NPCID.WallofFleshEye || 
                target.type == NPCID.SkeletronHead || target.type == NPCID.SkeletronHand)
            {
                target.buffImmune[BuffID.Venom] = false;
            }
            target.AddBuff(BuffID.Venom, 240);
        }
    }
}