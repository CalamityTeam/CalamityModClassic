using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;
//using TerrariaOverhaul;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class DarkechoGreatbow : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Darkecho Greatbow");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 37;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 34;
	        Item.height = 62;
	        Item.useTime = 22;
	        Item.useAnimation = 22;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 4;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
	        Item.UseSound = SoundID.Item5;
	        Item.autoReuse = true;
	        Item.shoot = 10;
	        Item.shootSpeed = 16f;
	        Item.useAmmo = 40;
	    }

        /*public void OverhaulInit()
        {
            this.SetTag("bow");
        }*/

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	for (int i = 0; i < 2; i++)
	    	{
	    		float SpeedX = velocity.X + (float) Main.rand.Next(-30, 31) * 0.05f;
	        	float SpeedY = velocity.Y + (float) Main.rand.Next(-30, 31) * 0.05f;
	    		switch (Main.rand.Next(3))
				{
	    			case 1: type = ProjectileID.UnholyArrow; break;
	    			default: break;
				}
                int index = Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
                Main.projectile[index].noDropItem = true;
            }
	    	return false;
		}
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "VerstaltiteBar", 8);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}