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
	public class TheEmpyrean : ModItem
	{
		public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	    {
		    //texture =("CalamityModClassic1Point1/Items/Weapons/TheEmpyrean");
		    return true;
	    }
	
	    public override void SetDefaults()
	    {
		    //Tooltip.SetDefault("The Empyrean");
			Item.damage = 82;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 66;
			Item.height = 32;
			////Tooltip.SetDefault("70% chance to not consume gel");
			Item.useTime = 5;
			Item.useAnimation = 15;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 3.5f;
			Item.UseSound = SoundID.Item34;
			Item.value = 900000;
			Item.rare = 9;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("CosmicFire").Type; //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 7.5f;
			Item.useAmmo = 23;
		}
	    
	    public override bool CanConsumeAmmo(Item ammo, Player player)
	    {
	    	if (Main.rand.Next(0, 100) <= 70)
	    		return false;
	    	return true;
	    }
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "MeldiateBar", 12);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}