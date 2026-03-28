using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.GreatSandShark
{
	public class Tumbleweed : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Tumbleweed");
            // Tooltip.SetDefault("Releases a rolling tumbleweed on enemy hits");
        }

	    public override void SetDefaults()
	    {
	        Item.damage = 110;
	        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
	        Item.width = 30;
	        Item.height = 10;
	        Item.useTime = 20;
	        Item.useAnimation = 20;
	        Item.useStyle = 5;
	        Item.noMelee = true;
            Item.noUseGraphic = true;
	        Item.knockBack = 8f;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.rare = 7;
	        Item.UseSound = SoundID.Item1;
	        Item.autoReuse = true;
            Item.channel = true;
	        Item.shoot = Mod.Find<ModProjectile>("Tumbleweed").Type;
	        Item.shootSpeed = 12f;
	    }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Sunfury);
            recipe.AddIngredient(null, "GrandScale");
            recipe.AddIngredient(ItemID.SoulofMight, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}