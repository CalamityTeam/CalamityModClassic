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
	public class NuclearFury : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Nuclear Fury");
			//Tooltip.SetDefault("Casts a torrent of cosmic typhoons");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 65;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 13;
	        Item.width = 28;
	        Item.height = 30;
	        Item.useTime = 25;
	        Item.useAnimation = 25;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 5;
	        Item.value = 2500000;
	        Item.rare = ItemRarityID.Cyan;
	        Item.UseSound = SoundID.Item84;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("NuclearFuryProjectile").Type;
	        Item.shootSpeed = 16f;
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	float spread = 45f * 0.0174f;
	    	double startAngle = Math.Atan2(velocity.X, velocity.Y)- spread/2;
	    	double deltaAngle = spread/8f;
	    	double offsetAngle;
	    	int i;
	    	for (i = 0; i < 4; i++ )
	    	{
	   			offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
	        	Projectile.NewProjectile(source, position.X, position.Y, (float)( Math.Sin(offsetAngle) * 5f ), (float)( Math.Cos(offsetAngle) * 5f ), type, damage, knockback, Main.myPlayer);
	        	Projectile.NewProjectile(source, position.X, position.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), type, damage, knockback, Main.myPlayer);
	    	}
	    	return false;
		}
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.FragmentNebula, 5);
	        recipe.AddIngredient(ItemID.SoulofSight, 10);
	        recipe.AddIngredient(ItemID.UnicornHorn, 5);
	        recipe.AddIngredient(ItemID.RazorbladeTyphoon);
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
	    }
	}
}