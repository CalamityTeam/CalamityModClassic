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
	public class AstralBlaster : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Astral Blaster");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 90;
	        Item.crit += 25;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 40;
	        Item.height = 24;
	        Item.useTime = 11;
	        Item.useAnimation = 11;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 2.75f;
	        Item.value = 350000;
	        Item.rare = ItemRarityID.Lime;
	        Item.UseSound = SoundID.Item41;
	        Item.autoReuse = true;
	        Item.shootSpeed = 14f;
	        Item.shoot = Mod.Find<ModProjectile>("AstralRound").Type;
	        Item.useAmmo = 97;
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("AstralRound").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
		}
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "AstralBar", 6);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}