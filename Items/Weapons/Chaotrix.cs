using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Chaotrix : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/Chaotrix");
		return true;
	}
	
    public override void SetDefaults()
    {
    	Item.CloneDefaults(ItemID.Yelets);
        //Tooltip.SetDefault("Chaotrix");
        Item.damage = 58;
        Item.useTime = 22;
        Item.useAnimation = 22;
        ////Tooltip.SetDefault("Explodes on enemy hits");
        Item.useStyle = 5;
        Item.channel = true;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.knockBack = 3.6f;
        Item.value = 300000;
        Item.rare = 7;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("ChaotrixProjectile").Type;
    }
    
    public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "CruptixBar", 6);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}