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
	public class GreatbowofTurmoil : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Greatbow of Turmoil");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 52;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 18;
	        Item.height = 36;
	        Item.useTime = 17;
	        Item.useAnimation = 17;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 4f;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.rare = 8;
	        Item.UseSound = SoundID.Item5;
	        Item.autoReuse = true;
	        Item.shoot = 10;
	        Item.shootSpeed = 17f;
	        Item.useAmmo = 40;
	    }

        /*public void OverhaulInit()
        {
            this.SetTag("bow");
        }*/

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	for (int i = 0; i < 3; i++)
	    	{
		    	float SpeedX = velocity.X + (float) Main.rand.Next(-30, 31) * 0.05f;
		       	float SpeedY = velocity.Y + (float) Main.rand.Next(-30, 31) * 0.05f;
		    	switch (Main.rand.Next(6))
				{
		    		case 1: type = ProjectileID.CursedArrow; break;
		    		case 2: type = ProjectileID.HellfireArrow; break;
		    		case 3: type = ProjectileID.IchorArrow; break;
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
	        recipe.AddIngredient(null, "CruptixBar", 10);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}