using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class BrimroseStaff : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/BrimroseStaff");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Brimrose Staff");
        Item.damage = 67;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 16;
        Item.width = 44;
        Item.height = 46;
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.useStyle = 5;
        Item.staff[Item.type] = true;
        ////Tooltip.SetDefault("Fires a spread of brimstone beams");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 7;
        Item.value = 300000;
        Item.rare = 6;
        Item.UseSound = SoundID.Item43;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("BrimstoneBeam").Type;
        Item.shootSpeed = 6f;
    }
    
    public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "UnholyCore", 6);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}