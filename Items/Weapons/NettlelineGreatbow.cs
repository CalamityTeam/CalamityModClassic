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
	public class NettlelineGreatbow : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Nettlevine Greatbow");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 189;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 30;
	        Item.height = 58;
	        Item.useTime = 17;
	        Item.useAnimation = 17;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 3f;
	        Item.value = 1200000;
	        Item.UseSound = SoundID.Item5;
	        Item.autoReuse = true;
	        Item.shoot = ProjectileID.PurificationPowder;
	        Item.shootSpeed = 16f;
	        Item.useAmmo = 40;
	    }
	    
	    public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(0, 255, 200);
	            }
	        }
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	        for(int i = 0; i < 5; i++)
	        {
	        	float SpeedX = velocity.X + (float) Main.rand.Next(-40, 41) * 0.05f;
	        	float SpeedY = velocity.Y + (float) Main.rand.Next(-40, 41) * 0.05f;
	    		switch (Main.rand.Next(4))
				{
	    			case 1: type = ProjectileID.VenomArrow; break;
	    			case 2: type = ProjectileID.ChlorophyteArrow; break;
	    			default: break;
				}
	    		Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
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