using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Permafrost
{
	public class EnchantedMetal : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Enchanted Metal");
		}
		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 20;
			Item.value = Item.buyPrice(0, 15, 0, 0);
            Item.rare = 5;
            Item.maxStack = 99;
		}
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemID.MechanicalEye);
            recipe.AddIngredient(Item.type, 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            recipe = Recipe.Create(ItemID.MechanicalSkull);
            recipe.AddIngredient(Item.type, 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            recipe = Recipe.Create(ItemID.MechanicalWorm);
            recipe.AddIngredient(Item.type, 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
