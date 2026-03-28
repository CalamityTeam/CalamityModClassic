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
	public class BrimroseStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Brimrose Staff");
			// Tooltip.SetDefault("Fires a spread of brimstone beams");
			Item.staff[Item.type] = true;
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 20;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 14;
	        Item.width = 36;
	        Item.height = 34;
	        Item.useTime = 30;
	        Item.useAnimation = 30;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 7f;
            Item.value = Item.buyPrice(0, 48, 0, 0);
            Item.rare = 6;
	        Item.UseSound = SoundID.Item43;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("BrimstoneBeam").Type;
	        Item.shootSpeed = 6f;
	    }
	    
	    public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "UnholyCore", 6);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	}
}