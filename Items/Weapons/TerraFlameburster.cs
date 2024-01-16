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
	public class TerraFlameburster : ModItem
	{
		public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	    {
		    //texture =("CalamityModClassic1Point1/Items/Weapons/TerraFlameburster");
		    return true;
	    }
	
	    public override void SetDefaults()
	    {
		    //Tooltip.SetDefault("Terra Flameburster");
			Item.damage = 70;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 68;
			Item.height = 22;
			////Tooltip.SetDefault("80% chance to not consume gel");
			Item.useTime = 3;
			Item.useAnimation = 15;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 3.25f;
			Item.UseSound = SoundID.Item34;
			Item.value = 1000000;
			Item.rare = 9;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("TerraFireGreen").Type; //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 7.5f;
			Item.useAmmo = 23;
		}
	    
	    public override bool CanConsumeAmmo(Item ammo, Player player)
	    {
	    	if (Main.rand.Next(0, 100) <= 80)
	    		return false;
	    	return true;
	    }
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.Flamethrower);
	        recipe.AddIngredient(null, "LivingShard", 5);
	        recipe.AddIngredient(null, "EssenceofCinder", 3);
	        recipe.AddIngredient(null, "PlagueCellCluster", 3);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}