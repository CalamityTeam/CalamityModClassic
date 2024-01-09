using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons 
{
	public class BrimroseStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Brimrose Staff");
			//Tooltip.SetDefault("Fires a spread of brimstone beams");
			Item.staff[Item.type] = true;
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 32;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 14;
	        Item.width = 44;
	        Item.height = 46;
	        Item.useTime = 30;
	        Item.useAnimation = 30;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 7;
	        Item.value = 300000;
	        Item.rare = ItemRarityID.LightPurple;
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