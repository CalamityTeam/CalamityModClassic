using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class CoralSpout : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/CoralSpout");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Coral Spout");
        Item.damage = 13;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 4;
        Item.width = 28;
        Item.height = 30;
        Item.useTime = 18;
        Item.useAnimation = 18;
        Item.useStyle = 5;
        ////Tooltip.SetDefault("Casts coral water spouts that lay on the ground and damage enemies");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 2.5f;
        Item.value = 50000;
        Item.rare = 2;
        Item.UseSound = SoundID.Item17;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("CoralSpike").Type;
        Item.shootSpeed = 16f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "VictideBar", 2);
        recipe.AddIngredient(ItemID.Coral, 5);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}}