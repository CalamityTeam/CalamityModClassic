using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
    public class TrueTyrantYharimsUltisword : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("True Tyrant's Ultisword");
            /* Tooltip.SetDefault("Contains the essence of a forgotten age\n" +
                "50% chance to give the player the tyrant's fury buff on enemy hits\n" +
                "This buff increases melee damage by 30% and melee crit chance by 10%"); */
        }

        public override void SetDefaults()
        {
            Item.width = 102;
            Item.damage = 185;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.useAnimation = 18;
            Item.useStyle = 1;
            Item.useTime = 18;
            Item.useTurn = true;
            Item.knockBack = 7.5f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.height = 102;
            Item.value = Item.buyPrice(1, 20, 0, 0);
            Item.rare = 10;
            Item.shoot = Mod.Find<ModProjectile>("BlazingPhantomBlade").Type;
            Item.shootSpeed = 12f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            switch (Main.rand.Next(3))
            {
                case 0: type = Mod.Find<ModProjectile>("BlazingPhantomBlade").Type; break;
                case 1: type = Mod.Find<ModProjectile>("HyperBlade").Type; break;
                case 2: type = Mod.Find<ModProjectile>("SunlightBlade").Type; break;
                default: break;
            }
            Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, Main.myPlayer);
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "TyrantYharimsUltisword");
            recipe.AddIngredient(null, "CoreofCalamity");
            recipe.AddIngredient(null, "UeliaceBar", 15);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(5) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 106);
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.Next(2) == 0)
            {
                player.AddBuff(Mod.Find<ModBuff>("TyrantsFury").Type, 300);
            }
            target.AddBuff(BuffID.OnFire, 300);
            target.AddBuff(BuffID.Venom, 240);
            target.AddBuff(BuffID.CursedInferno, 180);
            target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 300);
            target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 300);
        }
    }
}
