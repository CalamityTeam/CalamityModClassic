using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
	public class Drataliornus : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Drataliornus");
			/* Tooltip.SetDefault(@"Fires an escalating stream of fireballs.
Fireballs rain meteors, leave dragon dust trails, and launch additional bolts at max speed.
Taking damage while firing the stream will interrupt it and heavily debuff your stats.
Right click to fire two devastating barrages of five empowered fireballs.
'Just don't get hit'"); */
		}

		public override void SetDefaults()
		{
            Item.damage = 410;
			Item.knockBack = 1f;
            Item.shootSpeed = 18f;
			Item.useStyle = 5;
			Item.useAnimation = 10;
			Item.useTime = 10;
            Item.reuseDelay = 0;
            Item.width = 64;
			Item.height = 84;
			Item.UseSound = SoundID.Item5;
			Item.shoot = Mod.Find<ModProjectile>("Drataliornus").Type;
            Item.value = Item.buyPrice(5, 0, 0, 0);
            Item.rare = 10;
            Item.noMelee = true;
            Item.noUseGraphic = true;
			Item.DamageType = DamageClass.Ranged;
            Item.channel = true;
			Item.useTurn = false;
			Item.useAmmo = AmmoID.Arrow;
			Item.autoReuse = true;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 16;
		}

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useAnimation = 36;
                Item.useTime = 18;
                Item.reuseDelay = 72;
                Item.noUseGraphic = false;
            }
            else
            {
                Item.useAnimation = 9;
                Item.useTime = 9;
                Item.reuseDelay = 0;
                Item.noUseGraphic = true;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot (Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
            if (player.altFunctionUse == 2) //tsunami
            {
                const float num3 = 0.471238898f;
                const int num4 = 5;
                Vector2 spinningpoint = new Vector2(velocity.X, velocity.Y);
                spinningpoint.Normalize();
                spinningpoint *= 36f;
                for (int index1 = 0; index1 < num4; ++index1)
                {
                    float num8 = index1 - (num4 - 1) / 2;
                    Vector2 vector2_5 = spinningpoint.RotatedBy(num3 * num8, new Vector2());
                    Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X + vector2_5.X, position.Y + vector2_5.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("DrataliornusFlame").Type, damage, knockback, player.whoAmI, 1f, 0f);
                }
            }
            else
            {
                Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("Drataliornus").Type, 0, 0f, player.whoAmI);
            }
			
			return false;
		}

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(4f, 0f);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "BlossomFlux");
            recipe.AddIngredient(null, "DaemonsFlame");
            recipe.AddIngredient(null, "Deathwind");
            recipe.AddIngredient(null, "HeavenlyGale");
            recipe.AddIngredient(null, "DragonsBreath", 2);
            recipe.AddIngredient(null, "ChickenCannon", 2);
            recipe.AddIngredient(null, "AngryChickenStaff", 2);
            recipe.AddIngredient(null, "YharimsGift", 8);
            recipe.AddIngredient(null, "AuricOre", 80);
            recipe.AddIngredient(null, "EffulgentFeather", 160);
            recipe.AddIngredient(null, "DarksunFragment", 80);
            recipe.AddIngredient(null, "NightmareFuel", 80);
            recipe.AddIngredient(null, "EndothermicEnergy", 80);
            recipe.AddTile(null, "DraedonsForge");
            recipe.Register();
        }
    }
}
