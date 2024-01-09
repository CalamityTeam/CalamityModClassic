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
	public class MagnaCannon : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Magna Cannon");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 17;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 12;
	        Item.width = 56;
	        Item.height = 34;
	        Item.useTime = 32;
	        Item.useAnimation = 32;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 3.5f;
	        Item.value = 90000;
	        Item.rare = ItemRarityID.Orange;
	        Item.UseSound = SoundID.Item117;
	        Item.autoReuse = true;
	        Item.shootSpeed = 12f;
	        Item.shoot = Mod.Find<ModProjectile>("MagnaBlast").Type;
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	    {
	        int num6 = 3;
	        for (int index = 0; index < num6; ++index)
	        {
	            Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
	        }
	        return false;
		}
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.Granite, 25);
	        recipe.AddIngredient(ItemID.Obsidian, 15);
	        recipe.AddIngredient(ItemID.Amber, 5);
	        recipe.AddIngredient(ItemID.SpaceGun);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
	    }
	}
}