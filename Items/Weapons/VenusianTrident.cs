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
    public class VenusianTrident : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Venusian Trident");
            // Tooltip.SetDefault("Casts an inferno bolt that erupts into a gigantic explosion of fire and magma shards");
            Item.staff[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 225;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 20;
            Item.width = 48;
            Item.height = 48;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 9f;
            Item.value = Item.buyPrice(1, 40, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item45;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("VenusianBolt").Type;
            Item.shootSpeed = 19f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.InfernoFork);
            recipe.AddIngredient(null, "RuinousSoul", 2);
            recipe.AddIngredient(null, "TwistingNether");
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}