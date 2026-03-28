using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items.Patreon;

namespace CalamityModClassicPreTrailer.Items.Patreon
{
    [AutoloadEquip(EquipType.Body)]
    public class ApronOfAffection : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ace's Apron of Affection");
            // Tooltip.SetDefault("Great for hugging people");
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
			Item.rare = 5;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 21;
			Item.value = Item.buyPrice(0, 15, 0, 0);
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Robe);
            recipe.AddIngredient(ItemID.LovePotion, 10);
            recipe.AddIngredient(ItemID.LifeCrystal);
            recipe.AddTile(TileID.Loom);
            recipe.Register();
        }
    }
}