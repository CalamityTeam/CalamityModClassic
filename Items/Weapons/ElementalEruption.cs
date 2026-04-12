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
	public class ElementalEruption : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Elemental Eruption");
			// Tooltip.SetDefault("90% chance to not consume gel");
		}

	    public override void SetDefaults()
	    {
			Item.damage = 28;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 64;
			Item.height = 34;
			Item.useTime = 2;
			Item.useAnimation = 10;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 3.5f;
			Item.UseSound = SoundID.Item34;
            Item.value = Item.buyPrice(1, 20, 0, 0);
            Item.rare = 10;
            Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("TerraFireGreen2").Type;
			Item.shootSpeed = 10f;
			Item.useAmmo = 23;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	        int num6 = Main.rand.Next(3, 6);
	        for (int index = 0; index < num6; ++index)
	        {
	            float SpeedX = velocity.X + (float) Main.rand.Next(-25, 26) * 0.05f;
	            float SpeedY = velocity.Y + (float) Main.rand.Next(-25, 26) * 0.05f;
	    		switch (Main.rand.Next(3))
				{
	    			case 0: type = Mod.Find<ModProjectile>("TerraFireGreen2").Type; break;
	    			case 1: type = Mod.Find<ModProjectile>("TerraFireRed").Type; break;
	    			case 2: type = Mod.Find<ModProjectile>("TerraFireBlue").Type; break;
	    			default: break;
				}
	            Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	}
	    	return false;
		}
	    
	    public override bool CanConsumeAmmo(Item ammo, Player player)
	    {
	    	if (Main.rand.Next(0, 100) < 90)
	    		return false;
	    	return true;
	    }
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "GalacticaSingularity", 5);
	        recipe.AddIngredient(null, "TerraFlameburster");
	        recipe.AddIngredient(null, "Meowthrower");
	        recipe.AddIngredient(null, "MepheticSprayer");
	        recipe.AddIngredient(null, "BrimstoneFlamesprayer");
	        recipe.AddIngredient(ItemID.LunarBar, 5);
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
	    }
	}
}