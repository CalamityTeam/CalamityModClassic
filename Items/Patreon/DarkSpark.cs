using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Patreon 
{
	public class DarkSpark : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dark Spark");
			/* Tooltip.SetDefault("And everything under the sun is in tune,\n" +
                "But the sun is eclipsed by the moon."); */
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 4));
            ItemID.Sets.AnimatesAsSoul[Type] = true;
        }

	    public override void SetDefaults()
	    {
	        Item.damage = 100;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 10;
	        Item.width = 16;
	        Item.height = 16;
	        Item.useTime = 10;
	        Item.useAnimation = 10;
	        Item.reuseDelay = 5;
	        Item.useStyle = 4;
	        Item.UseSound = SoundID.Item13;
	        Item.noMelee = true;
	        Item.noUseGraphic = true;
			Item.channel = true;
	        Item.knockBack = 0f;
            Item.value = Item.buyPrice(1, 40, 0, 0);
            Item.rare = 10;
            Item.shoot = Mod.Find<ModProjectile>("DarkSpark").Type;
	        Item.shootSpeed = 30f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 21;
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("DarkSpark").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
            return false;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LastPrism);
            recipe.AddIngredient(null, "DarkPlasma", 10);
            recipe.AddIngredient(null, "RuinousSoul", 20);
            recipe.AddIngredient(null, "DivineGeode", 30);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}