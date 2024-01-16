using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Goobow : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/4.SlimeGod/Goobow");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Goobow");
        Item.damage = 36;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 20;
        Item.height = 36;
        Item.useTime = 29;
        Item.useAnimation = 29;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 4.5f;
        Item.value = 105000;
        Item.rare = 5;
        Item.UseSound = SoundID.Item5;
        Item.autoReuse = true;
        Item.shoot = 10; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 12f;
        Item.useAmmo = 40;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "PurifiedGel", 18);
        recipe.AddIngredient(ItemID.Gel, 30);
        recipe.AddIngredient(ItemID.HellstoneBar, 5);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}}