using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
    public class DraedonsExoblade : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Exoblade");
            /* Tooltip.SetDefault("Ancient blade of Yharim's weapons and armors expert, Draedon\n" +
                               "Fires an exo beam that homes in on the player and explodes\n" +
                               "Striking an enemy with the blade causes several comets to fire\n" +
                               "All attacks freeze any enemy in place for several seconds at a 10% chance\n" +
                               "Enemies hit at very low HP explode into frost energy and freeze nearby enemies\n" +
                               "The lower your HP the more damage this blade does and heals the player on enemy hits"); */
        }

        public override void SetDefaults()
        {
            Item.width = 80;
            Item.damage = 6700;
            Item.useAnimation = 14;
            Item.useStyle = 1;
            Item.useTime = 14;
            Item.useTurn = true;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.knockBack = 9f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.height = 114;
            Item.value = Item.buyPrice(2, 50, 0, 0);
            Item.rare = 10;
            Item.shoot = Mod.Find<ModProjectile>("Exobeam").Type;
            Item.shootSpeed = 19f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 15;
		}

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            int lifeAmount = player.statLifeMax2 - player.statLife;
            float damageAdd = ((float)lifeAmount + (float)Item.damage);
            damage.Base = (int)(damageAdd * player.GetDamage(DamageClass.Melee).Base);
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(4) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 107, 0f, 0f, 100, new Color(0, 255, 255));
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.life <= (target.lifeMax * 0.05f))
            {
                Projectile.NewProjectile(Entity.GetSource_FromThis(null), target.Center.X, target.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("Exoboom").Type, (int)((float)Item.damage * player.GetDamage(DamageClass.Melee).Multiplicative), Item.knockBack, Main.myPlayer);
            }
            if (Main.rand.Next(5) == 0)
            {
                target.AddBuff(Mod.Find<ModBuff>("ExoFreeze").Type, 500);
            }
            target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 200);
            target.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 200);
            target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 200);
            target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 200);
            target.AddBuff(BuffID.CursedInferno, 200);
            target.AddBuff(BuffID.Frostburn, 200);
            target.AddBuff(BuffID.OnFire, 200);
            target.AddBuff(BuffID.Ichor, 200);
            SoundEngine.PlaySound(SoundID.Item88, player.position);
            float xPos = (Main.rand.Next(2) == 0) ? player.position.X + 800 : player.position.X - 800;
            Vector2 vector2 = new Vector2(xPos, player.position.Y + Main.rand.Next(-800, 801));
            float num80 = xPos;
            float speedX = (float)target.position.X - vector2.X;
            float speedY = (float)target.position.Y - vector2.Y;
            float dir = (float)Math.Sqrt((double)(speedX * speedX + speedY * speedY));
            dir = 10 / num80;
            speedX *= dir * 150;
            speedY *= dir * 150;
            if (speedX > 15f)
            {
                speedX = 15f;
            }
            if (speedX < -15f)
            {
                speedX = -15f;
            }
            if (speedY > 15f)
            {
                speedY = 15f;
            }
            if (speedY < -15f)
            {
                speedY = -15f;
            }
            if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("Exocomet").Type] < 8)
            {
                for (int comet = 0; comet < 2; comet++)
                {
                    float ai1 = (Main.rand.NextFloat() + 0.5f);
                    Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, Item.velocity.X, Item.velocity.Y, Mod.Find<ModProjectile>("Exocomet").Type, (int)((float)Item.damage * player.GetDamage(DamageClass.Melee).Multiplicative), Item.knockBack, player.whoAmI, 0.0f, ai1);
                }
            }
            if (target.type == NPCID.TargetDummy || !target.canGhostHeal)
            {
                return;
            }
            int healAmount = (Main.rand.Next(5) + 5);
            player.statLife += healAmount;
            player.HealEffect(healAmount);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Terratomere");
            recipe.AddIngredient(null, "AnarchyBlade");
            recipe.AddIngredient(null, "BalefulHarvester");
            recipe.AddIngredient(null, "FlarefrostBlade");
            recipe.AddIngredient(null, "PhoenixBlade");
            recipe.AddIngredient(null, "StellarStriker");
            recipe.AddIngredient(null, "NightmareFuel", 5);
            recipe.AddIngredient(null, "EndothermicEnergy", 5);
            recipe.AddIngredient(null, "CosmiliteBar", 5);
            recipe.AddIngredient(null, "DarksunFragment", 5);
            recipe.AddIngredient(null, "HellcasterFragment", 3);
            recipe.AddIngredient(null, "Phantoplasm", 5);
            recipe.AddIngredient(null, "AuricOre", 25);
            recipe.AddTile(null, "DraedonsForge");
            recipe.Register();
        }
    }
}
