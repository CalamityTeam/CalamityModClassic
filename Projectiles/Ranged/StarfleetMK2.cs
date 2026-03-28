using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Ranged
{
	public class StarfleetMK2 : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Starmada");
        }

        public override void SetDefaults()
		{
			Projectile.width = 122;
			Projectile.height = 50;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 32f / 255f, 208f / 255f, 255f / 255f);
            Player player = Main.player[Projectile.owner];
            Projectile.ai[1]--;
            if (Projectile.ai[0] >= 0f)
            {
                Projectile.ai[0] += 1f;
                switch ((int)Projectile.ai[0])
                {
                    case 90:
                    case 180:
                    case 270:
                    case 360:
                    case 450: Projectile.localAI[0] += 1f; break;
                    case 540:
                        Projectile.localAI[0] += 1f;
                        Projectile.ai[0] = -1f;
                        SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
                        int num226 = 36;
                        for (int num227 = 0; num227 < num226; num227++)
                        {
                            Vector2 vector6 = Vector2.Normalize(Projectile.velocity) * 9f;
                            vector6 = vector6.RotatedBy(((num227 - (num226 / 2 - 1)) * 6.28318548f / num226), default(Vector2)) + player.Center;
                            Vector2 vector7 = vector6 - player.Center;
                            int num228 = Dust.NewDust(vector6 + vector7, 0, 0, 221, 0f, 0f, 0, default(Color), 4f);
                            Main.dust[num228].noGravity = true;
                            Main.dust[num228].velocity = vector7;
                        }
                        break;
                }
            }
            int baseUseTime = 27;
            int modifier = 2;
            bool timeToFire = false;
            if (Projectile.ai[1] <= 0f)
            {
                Projectile.ai[1] = baseUseTime - modifier * Projectile.localAI[0];
                timeToFire = true;
            }
            bool canFire = player.channel && player.HasAmmo(player.inventory[player.selectedItem]) && !player.noItems && !player.CCed;
            if (Projectile.soundDelay <= 0 && canFire)
            {
                Projectile.soundDelay = baseUseTime - modifier * (int)Projectile.localAI[0];
                if (Projectile.ai[0] != 1f)
                    SoundEngine.PlaySound(SoundID.Item92, Projectile.position);
            }
            player.phantasmTime = 2;
            if (timeToFire && Main.myPlayer == Projectile.owner)
            {
                if (canFire)
                {
                    int type = ProjectileID.StarCannonStar;
                    float scaleFactor = 16f;
                    int damage = player.GetWeaponDamage(player.inventory[player.selectedItem]);
                    float knockBack = player.inventory[player.selectedItem].knockBack;
                    player.PickAmmo(player.inventory[player.selectedItem], out type, out scaleFactor, out damage, out knockBack, out _, false);
                    for (int i = 0; i < 5; i++)
                    {
                        knockBack = player.GetWeaponKnockback(player.inventory[player.selectedItem], knockBack);
                        Vector2 playerPosition = player.RotatedRelativePoint(player.MountedCenter, true);
                        Projectile.velocity = Main.screenPosition - playerPosition;
                        Projectile.velocity.X += Main.mouseX;
                        Projectile.velocity.Y += Main.mouseY;
                        if (player.gravDir == -1f)
                            Projectile.velocity.Y = (Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - playerPosition.Y;
                        Projectile.velocity.Normalize();
                        float variation = (1f + Projectile.localAI[0]) * 3f;
                        Vector2 position = playerPosition + Utils.RandomVector2(Main.rand, -variation, variation);
                        Vector2 speed = Projectile.velocity * scaleFactor * (0.6f + Main.rand.NextFloat() * 0.6f);
                        switch (Main.rand.Next(5))
                        {
                            case 0: type = Mod.Find<ModProjectile>("PlasmaBlast").Type; break;
                            case 1: type = Mod.Find<ModProjectile>("AstralStar").Type; break;
                            case 2: type = Mod.Find<ModProjectile>("GalacticaComet").Type; break;
                            case 3: type = ProjectileID.StarCannonStar; break; // thanks starmada very cool
                            case 4: type = 9; break;
                        }
						int star = Projectile.NewProjectile(Projectile.GetSource_FromThis(null), position, speed, type, damage, knockBack, Projectile.owner, 0f, 0f);
						Main.projectile[star].penetrate = 1;
						Main.projectile[star].timeLeft = 300;
						Main.projectile[star].GetGlobalProjectile<CalamityGlobalProjectile>().forceRanged = true;
						Main.projectile[star].netUpdate = true;
                        Projectile.netUpdate = true;
                    }
                }
                else
                {
                    Projectile.Kill();
                }
            }
            Projectile.rotation = Projectile.velocity.ToRotation();
            Vector2 displayOffset = new Vector2(32, 0).RotatedBy(Projectile.rotation);
            Projectile.Center = player.RotatedRelativePoint(player.MountedCenter, true) + displayOffset;
            if (Projectile.spriteDirection == -1)
                Projectile.rotation += 3.14159274f;
            Projectile.spriteDirection = Projectile.direction;
            Projectile.timeLeft = 2;
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2(Projectile.velocity.Y * Projectile.direction, Projectile.velocity.X * Projectile.direction);
        }

		public override bool? CanDamage()/* tModPorter Suggestion: Return null instead of true */
        {
            return false;
        }
    }
}