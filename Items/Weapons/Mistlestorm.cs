using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class Mistlestorm : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mistlestorm");
			// Tooltip.SetDefault("Casts a storm of pine needles and leaves");
			Item.staff[Item.type] = true;
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 60;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 5;
	        Item.width = 48;
	        Item.height = 48;
	        Item.useTime = 6;
	        Item.useAnimation = 6;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 3.5f;
            Item.value = Item.buyPrice(1, 40, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item39;
	        Item.autoReuse = true;
	        Item.shoot = 336;
	        Item.shootSpeed = 24f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int num106 = 2 + Main.rand.Next(3);
            for (int num107 = 0; num107 < num106; num107++)
            {
                float num110 = 0.025f * (float)num107;
                velocity.X += (float)Main.rand.Next(-35, 36) * num110;
                velocity.Y += (float)Main.rand.Next(-35, 36) * num110;
                float num84 = (float)Math.Sqrt((double)(velocity.X * velocity.X + velocity.Y * velocity.Y));
                num84 = Item.shootSpeed / num84;
                velocity.X *= num84;
                velocity.Y *= num84;
                Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, (float)Main.rand.Next(0, 10 * (num107 + 1)), 0f);
                Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, 206, damage, knockback, player.whoAmI, 0.0f, 0.0f);
            }
            return false;
        }

        public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.Razorpine);
            recipe.AddIngredient(ItemID.LeafBlower);
            recipe.AddIngredient(null, "UeliaceBar", 7);
            recipe.AddIngredient(null, "DarkPlasma");
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
	    }
	}
}