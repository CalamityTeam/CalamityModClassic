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
	public class StormSurge : ModItem
	{
		public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	    {
		    //texture =("CalamityModClassic1Point1/Items/Weapons/StormSurge");
		    return true;
	    }
	
	    public override void SetDefaults()
	    {
		    //Tooltip.SetDefault("Storm Surge");
			Item.damage = 27;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 58;
			Item.height = 22;
			////Tooltip.SetDefault("Fear the storm\nDoes not consume ammo");
			Item.useTime = 22;
			Item.useAnimation = 22;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 5f;
			Item.UseSound = SoundID.Item122;
			Item.value = 10000;
			Item.rare = 2;
			Item.autoReuse = false;
			Item.shoot = Mod.Find<ModProjectile>("StormSurge").Type; //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 10f;
		}
	    
	    public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "StormlionMandible");
			recipe.AddIngredient(null, "VictideBar", 2);
			recipe.AddIngredient(null, "AerialiteBar", 4);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
		}
	}
}