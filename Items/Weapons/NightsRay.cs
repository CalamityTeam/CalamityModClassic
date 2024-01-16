using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class NightsRay : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/NightsRay");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Night's Ray");
        Item.damage = 26;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 10;
        Item.width = 56;
        Item.height = 56;
        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.useStyle = 5;
        Item.staff[Item.type] = true;
        ////Tooltip.SetDefault("Fires a beam of dark energy that can split into additional beams\nEnemies must be near for the beam to split");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 3.25f;
        Item.value = 100000;
        Item.rare = 5;
        Item.UseSound = SoundID.Item72;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("NightRay").Type;
        Item.shootSpeed = 6f;
    }
    
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.WandofSparking);
        recipe.AddIngredient(ItemID.Vilethorn);
        recipe.AddIngredient(ItemID.AmberStaff);
        recipe.AddIngredient(ItemID.MagicMissile);
        recipe.AddIngredient(null, "TrueShadowScale", 15);
        recipe.AddTile(TileID.DemonAltar);
        recipe.Register();
        recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.WandofSparking);
        recipe.AddIngredient(ItemID.CrimsonRod);
        recipe.AddIngredient(ItemID.AmberStaff);
        recipe.AddIngredient(ItemID.MagicMissile);
        recipe.AddIngredient(null, "BloodSample", 15);
        recipe.AddTile(TileID.DemonAltar);
        recipe.Register();
    }
}}