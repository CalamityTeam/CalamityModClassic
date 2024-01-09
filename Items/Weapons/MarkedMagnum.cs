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
	public class MarkedMagnum : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Marked Magnum");
			//Tooltip.SetDefault("Shots reduce enemy protection\nProjectile damage is multiplied by all of your damage bonuses");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 8;
	        Item.width = 54;
	        Item.height = 20;
	        Item.useTime = 15;
	        Item.useAnimation = 15;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 3f;
	        Item.value = 30000;
	        Item.rare = ItemRarityID.Green;
	        Item.UseSound = SoundID.Item33;
	        Item.autoReuse = false;
	        Item.shootSpeed = 12f;
	        Item.shoot = Mod.Find<ModProjectile>("MarkRound").Type;
	    }
	    
	    public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	float damageMult = player.GetDamage(DamageClass.Melee).Additive + player.GetDamage(DamageClass.Ranged).Additive + player.GetDamage(DamageClass.Magic).Additive + player.GetDamage(DamageClass.Throwing).Additive + player.GetDamage(DamageClass.Summon).Additive;
	    	Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, (int)((double)damage * damageMult), knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
		}
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.HellstoneBar, 7);
	        recipe.AddIngredient(ItemID.Obsidian, 15);
	        recipe.AddIngredient(ItemID.GlowingMushroom, 15);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
	    }
	}
}