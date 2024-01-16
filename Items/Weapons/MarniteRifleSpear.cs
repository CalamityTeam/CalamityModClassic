using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class MarniteRifleSpear : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/MarniteRifleSpear");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Marnite Rifle Spear");
        Item.damage = 15;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 72;
        Item.height = 20;
        Item.useTime = 28;
        Item.useAnimation = 28;
        Item.useStyle = 5;
        Item.knockBack = 2.25f;
        Item.value = 40000;
        Item.rare = 2;
        Item.UseSound = SoundID.Item41;
        Item.autoReuse = true;
        Item.shootSpeed = 22f;
        Item.useAmmo = 97;
        Item.shoot = 10;
    }
    
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.PlatinumBar, 7);
		recipe.AddIngredient(ItemID.Granite, 5);
		recipe.AddIngredient(ItemID.Marble, 5);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
        recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.GoldBar, 7);
		recipe.AddIngredient(ItemID.Granite, 5);
		recipe.AddIngredient(ItemID.Marble, 5);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}}