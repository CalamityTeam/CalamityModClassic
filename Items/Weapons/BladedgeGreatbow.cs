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
	public class BladedgeGreatbow : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Bladedge Railbow");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 26;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 74;
	        Item.height = 22;
	        Item.useTime = 24;
	        Item.useAnimation = 24;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 3.5f;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.rare = 7;
	        Item.UseSound = SoundID.Item5;
	        Item.autoReuse = true;
	        Item.shoot = 10;
	        Item.shootSpeed = 16f;
	        Item.useAmmo = 40;
	    }

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	for (int i = 0; i < 5; i++)
	    	{
	            float SpeedX = velocity.X + (float) Main.rand.Next(-60, 61) * 0.05f;
	            float SpeedY = velocity.Y + (float) Main.rand.Next(-60, 61) * 0.05f;
	    		switch (Main.rand.Next(5))
				{
	    			case 1: type = ProjectileID.ChlorophyteArrow; break;
	    			default: break;
				}
                int index = Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
                Main.projectile[index].noDropItem = true;
            }
	    	return false;
		}
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "DraedonBar", 12);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}