using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class HellBurst : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/HellBurst");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Hell Burst");
        Item.damage = 46;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 14;
        Item.width = 52;
        Item.height = 52;
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.useStyle = 1;
        ////Tooltip.SetDefault("Casts a beam of flame");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 7f;
        Item.value = 600000;
        Item.rare = 5;
        Item.UseSound = SoundID.Item34;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("FlameBeamTip").Type;
        Item.shootSpeed = 32f;
    }
    
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Flamelash);
        recipe.AddIngredient(ItemID.CrystalVileShard);
        recipe.AddIngredient(ItemID.DarkShard, 2);
        recipe.AddIngredient(ItemID.SoulofNight, 10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}