using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class PrimordialEarth : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/PrimordialEarth");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Primordial Earth");
        Item.damage = 88;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 19;
        Item.width = 28;
        Item.height = 30;
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.useStyle = 5;
        ////Tooltip.SetDefault("An ancient relic from an ancient land");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 7;
        Item.value = 8500000;
        Item.rare = 10;
        Item.UseSound = SoundID.Item20;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("SupremeDustProjectile").Type;
        Item.shootSpeed = 4f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "DeathValley");
        recipe.AddIngredient(ItemID.AncientBattleArmorMaterial, 3);
        recipe.AddIngredient(ItemID.MeteoriteBar, 5);
        recipe.AddIngredient(ItemID.Ectoplasm, 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}