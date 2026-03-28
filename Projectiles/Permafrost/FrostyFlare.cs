using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Permafrost
{
	public class FrostyFlare : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 10;
			Projectile.height = 10;
            Projectile.coldDamage = true;
            Projectile.friendly = true;
			Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
			Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().rogue = true;
		}
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Frosty Flare");
        }

		public override void AI()
		{
            bool shoot = false;
            Projectile.localAI[0]--;
            if (Projectile.localAI[0] <= 0f)
            {
                Projectile.localAI[0] = 30f;
                if (Projectile.owner == Main.myPlayer)
                    shoot = true;
            }

            if (Projectile.ai[0] == 0f)
            {
                Projectile.velocity.X *= 0.99f;
                Projectile.velocity.Y += 0.35f;
                Projectile.rotation = Projectile.velocity.ToRotation();

                if (shoot)
                {
                    Vector2 vel = new Vector2(Main.rand.Next(-300, 301), Main.rand.Next(500, 801));
                    Vector2 pos = Projectile.Center - vel;
                    vel.X += Main.rand.Next(-50, 51);
                    vel.Normalize();
                    vel *= 30f;
                    int p = Projectile.NewProjectile(Projectile.GetSource_FromThis(null), pos, vel + Projectile.velocity / 4f, Mod.Find<ModProjectile>("FrostShardFriendly").Type, Projectile.damage, Projectile.knockBack, Projectile.owner);
                    Main.projectile[p].minion = false;
                }

                int index2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 172);
                Main.dust[index2].noGravity = true;
            }
            else
            {
                Projectile.ignoreWater = true;
                Projectile.tileCollide = false;
                int id = (int)Projectile.ai[1];
                if (id >= 0 && id < 200 && Main.npc[id].active && !Main.npc[id].dontTakeDamage)
                {
                    Projectile.Center = Main.npc[id].Center - Projectile.velocity * 2f;
                    Projectile.gfxOffY = Main.npc[id].gfxOffY;

                    if (shoot)
                    {
                        Vector2 vel = new Vector2(Main.rand.Next(-300, 301), Main.rand.Next(500, 801));
                        Vector2 pos = Main.npc[id].Center - vel;
                        vel.X += Main.rand.Next(-50, 51);
                        vel.Normalize();
                        vel *= 30f;
                        int p = Projectile.NewProjectile(Projectile.GetSource_FromThis(null), pos, vel + Main.npc[id].velocity, Mod.Find<ModProjectile>("FrostShardFriendly").Type, Projectile.damage, Projectile.knockBack, Projectile.owner);
                        Main.projectile[p].minion = false;
                    }
                }
                else
                {
                    Projectile.Kill();
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn, 300);
            target.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 120);
            target.immune[Projectile.owner] = 0;
            Projectile.ai[0] = 1f;
            Projectile.ai[1] = target.whoAmI;
            Projectile.velocity = target.Center - Projectile.Center;
            Projectile.velocity *= 0.75f;
            Projectile.netUpdate = true;

            const int maxFlares = 5;
            int flaresFound = 0;
            int oldestFlare = -1;
            int oldestFlareTimeLeft = 300;
            for (int i = 0; i < 1000; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == Projectile.type && i != Projectile.whoAmI && Main.projectile[i].ai[1] == target.whoAmI)
                {
                    flaresFound++;
                    if (Main.projectile[i].timeLeft < oldestFlareTimeLeft)
                    {
                        oldestFlareTimeLeft = Main.projectile[i].timeLeft;
                        oldestFlare = Main.projectile[i].whoAmI;
                    }
                    if (flaresFound >= maxFlares)
                        break;
                }
            }
            //Main.NewText("found " + flaresFound.ToString());
            if (flaresFound >= maxFlares && oldestFlare >= 0)
            {
                //Main.NewText("killing flare " + oldestFlare.ToString());
                Main.projectile[oldestFlare].Kill();
            }
        }

        public override bool? CanDamage()/* tModPorter Suggestion: Return null instead of true */
        {
            return Projectile.ai[0] == 0f;
        }
	}
}