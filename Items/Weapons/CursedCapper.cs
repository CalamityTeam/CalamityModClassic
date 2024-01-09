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
	public class CursedCapper : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Cursed Capper");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 30;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 44;
	        Item.height = 32;
	        Item.useTime = 10;
	        Item.useAnimation = 10;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 2.25f;
	        Item.value = 150000;
	        Item.rare = ItemRarityID.Pink;
	        Item.UseSound = SoundID.Item41;
	        Item.autoReuse = false;
	        Item.shootSpeed = 14f;
	        Item.shoot = Mod.Find<ModProjectile>("CursedRound").Type;
	        Item.useAmmo = 97;
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("CursedRound").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
		}
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.PhoenixBlaster);
	        recipe.AddIngredient(ItemID.CursedFlame, 10);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}