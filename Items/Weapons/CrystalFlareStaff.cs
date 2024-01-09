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
	public class CrystalFlareStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Crystal Flare Staff");
			//Tooltip.SetDefault("Fires blue frost flames that explode");
			Item.staff[Item.type] = true;
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 45;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 15;
	        Item.width = 42;
	        Item.height = 42;
	        Item.useTime = 12;
	        Item.useAnimation = 24;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 5.25f;
	        Item.value = 650000;
	        Item.rare = ItemRarityID.LightPurple;
	        Item.UseSound = SoundID.Item20;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("SpiritFlameCurse").Type;
	        Item.shootSpeed = 14f;
	    }
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "EssenceofEleum", 3);
	        recipe.AddIngredient(ItemID.CrystalShard, 15);
	        recipe.AddIngredient(ItemID.FrostStaff);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}