using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items
{
    public class ShadowspecBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Shadowspec Bar");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(8, 10));
            ItemID.Sets.AnimatesAsSoul[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 15;
            Item.height = 12;
            Item.maxStack = 999;
			Item.rare = 10;
			Item.value = Item.buyPrice(0, 10, 0, 0);
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 15;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(3);
            recipe.AddIngredient(null, "BarofLife", 3);
            recipe.AddIngredient(null, "Phantoplasm", 3);
            recipe.AddIngredient(null, "NightmareFuel", 3);
            recipe.AddIngredient(null, "EndothermicEnergy", 3);
            recipe.AddIngredient(null, "CalamitousEssence");
            recipe.AddIngredient(null, "DarksunFragment");
            recipe.AddIngredient(null, "HellcasterFragment");
            recipe.AddTile(null, "DraedonsForge");
            recipe.Register();
        }
    }
}