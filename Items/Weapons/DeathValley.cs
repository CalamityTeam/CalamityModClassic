using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class DeathValley : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/DeathValley");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Death Valley Duster");
        Item.damage = 65;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 9;
        Item.width = 28;
        Item.height = 30;
        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.useStyle = 5;
        ////Tooltip.SetDefault("Casts a large blast of dust");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 5;
        Item.value = 1500000;
        Item.rare = 8;
        Item.UseSound = SoundID.Item20;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("DustProjectile").Type;
        Item.shootSpeed = 4f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "Tradewinds");
        recipe.AddIngredient(ItemID.FossilOre, 25);
        recipe.AddIngredient(ItemID.AncientBattleArmorMaterial);
        recipe.AddIngredient(null, "DesertFeather", 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}