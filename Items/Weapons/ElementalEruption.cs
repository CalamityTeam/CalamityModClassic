using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons
{
	public class ElementalEruption : ModItem
	{
		public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	    {
		    //texture =("CalamityModClassic1Point1/Items/Weapons/ElementalEruption");
		    return true;
	    }
	
	    public override void SetDefaults()
	    {
		    //Tooltip.SetDefault("Elemental Eruption");
			Item.damage = 60;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 68;
			Item.height = 22;
			////Tooltip.SetDefault("90% chance to not consume gel");
			Item.useTime = 2;
			Item.useAnimation = 10;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 3.5f;
			Item.UseSound = SoundID.Item34;
			Item.value = 10000000;
			Item.rare = 10;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("TerraFireGreen2").Type; //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 10f;
			Item.useAmmo = 23;
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
	            Projectile.NewProjectile(source, position, new Vector2(SpeedX, SpeedY),type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	}
	    	return false;
		}
	    
	    public override bool CanConsumeAmmo(Item ammo, Player player)
	    {
	    	if (Main.rand.Next(0, 100) <= 90)
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
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}