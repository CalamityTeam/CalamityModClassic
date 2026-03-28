using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Accessories
{
    public class DraedonsHeart : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Draedon's Heart");
            /* Tooltip.SetDefault("Gives 10% increased damage while you have the heart attack debuff\n" +
                "Increases your chance of getting the heart attack debuff\n" +
				"Boosts your damage by 10% and max movement speed and acceleration by 5%\n" +
                "Rage mode does more damage\n" +
                "You gain rage over time\n" +
                "Gives immunity to the horror debuff\n" +
                "Standing still regenerates your life quickly and boosts your defense by 25"); */
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 7));
            ItemID.Sets.AnimatesAsSoul[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.accessory = true;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 15;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.draedonsHeart = true;
            player.buffImmune[Mod.Find<ModBuff>("Horror").Type] = true;
            modPlayer.draedonsStressGain = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "HeartofDarkness");
            recipe.AddIngredient(null, "StressPills");
            recipe.AddIngredient(null, "Laudanum");
			recipe.AddIngredient(null, "CosmiliteBar", 5);
			recipe.AddIngredient(null, "Phantoplasm", 5);
			recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}