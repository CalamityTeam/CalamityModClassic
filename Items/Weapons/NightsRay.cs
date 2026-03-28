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
	public class NightsRay : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Night's Ray");
			Item.staff[Item.type] = true;
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 35;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 10;
	        Item.width = 50;
	        Item.height = 50;
	        Item.useTime = 20;
	        Item.useAnimation = 20;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 3.25f;
            Item.value = Item.buyPrice(0, 12, 0, 0);
            Item.rare = 4;
	        Item.UseSound = SoundID.Item72;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("NightRay").Type;
	        Item.shootSpeed = 6f;
	    }
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.WandofSparking);
	        recipe.AddIngredient(ItemID.Vilethorn);
	        recipe.AddIngredient(ItemID.AmberStaff);
	        recipe.AddIngredient(ItemID.MagicMissile);
	        recipe.AddIngredient(null, "TrueShadowScale", 15);
	        recipe.AddIngredient(null, "PurifiedGel", 10);
	        recipe.AddTile(TileID.DemonAltar);
	        recipe.Register();
	    }
	}
}