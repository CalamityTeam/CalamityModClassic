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
	public class NettlelineGreatbow : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Nettlevine Greatbow");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 120;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 36;
	        Item.height = 64;
	        Item.useTime = 17;
	        Item.useAnimation = 17;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 3f;
            Item.value = Item.buyPrice(1, 20, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item5;
	        Item.autoReuse = true;
	        Item.shoot = 10;
	        Item.shootSpeed = 16f;
	        Item.useAmmo = 40;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	        for(int i = 0; i < 5; i++)
	        {
	        	float SpeedX = velocity.X + (float) Main.rand.Next(-40, 41) * 0.05f;
	        	float SpeedY = velocity.Y + (float) Main.rand.Next(-40, 41) * 0.05f;
                if (type == ProjectileID.WoodenArrowFriendly)
                {
                    switch (Main.rand.Next(4))
                    {
                        case 1: type = ProjectileID.VenomArrow; break;
                        case 2: type = ProjectileID.ChlorophyteArrow; break;
                        default: break;
                    }
                    int index = Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
                    Main.projectile[index].noDropItem = true;
                }
                else
                {
                    int num121 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
                    Main.projectile[num121].noDropItem = true;
                }
            }
	    	return false;
		}
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "UeliaceBar", 12);
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
	    }
	}
}