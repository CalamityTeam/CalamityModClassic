using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class DemonicPitchfork : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/DemonicPitchfork");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Demonic Pitchfork");
        Item.damage = 67;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 22;
        Item.width = 56;
        Item.height = 56;
        Item.useTime = 28;
        Item.useAnimation = 28;
        Item.useStyle = 5;
        Item.staff[Item.type] = true;
        ////Tooltip.SetDefault("Fires a demonic pitchfork");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 6;
        Item.value = 450000;
        Item.rare = 6;
        Item.UseSound = SoundID.Item20;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("DemonicPitchfork").Type;
        Item.shootSpeed = 10f;
    }
    
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "TrueShadowScale", 15);
        recipe.AddIngredient(ItemID.DarkLance);
        recipe.AddIngredient(ItemID.Obsidian, 20);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}