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
	public class DeathValley : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Death Valley Duster");
			// Tooltip.SetDefault("Casts a large blast of dust");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 97;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 9;
	        Item.width = 28;
	        Item.height = 30;
	        Item.useTime = 25;
	        Item.useAnimation = 25;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 5f;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
	        Item.UseSound = SoundID.Item20;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("DustProjectile").Type;
	        Item.shootSpeed = 5f;
	    }
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "Tradewinds");
	        recipe.AddIngredient(ItemID.FossilOre, 25);
	        recipe.AddIngredient(ItemID.AncientBattleArmorMaterial);
	        recipe.AddIngredient(null, "DesertFeather", 5);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}