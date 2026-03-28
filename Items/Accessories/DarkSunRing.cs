using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;

namespace CalamityModClassicPreTrailer.Items.Accessories
{
    public class DarkSunRing : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dark Sun Ring");
            /* Tooltip.SetDefault("12% increase to damage, minion knockback, and melee speed\n" +
				"+1 life regen, 15% increased pick speed, and +2 max minions\n" +
                "During the day the player has +3 life regen\n" +
                "During the night the player has +30 defense\n" +
                "Contains the power of the dark sun"); */
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 6));
            ItemID.Sets.AnimatesAsSoul[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.value = Item.buyPrice(0, 90, 0, 0);
			Item.defense = 10;
			Item.lifeRegen = 1;
            Item.accessory = true;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 15;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.darkSunRing = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "UeliaceBar", 10);
            recipe.AddIngredient(null, "DarksunFragment", 100);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}