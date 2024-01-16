using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class CrystalFlareStaff : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/CrystalFlareStaff");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Crystal Flare Staff");
        Item.damage = 58;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 15;
        Item.width = 42;
        Item.height = 42;
        Item.useTime = 10;
        Item.useAnimation = 20;
        Item.useStyle = 5;
        Item.staff[Item.type] = true;
        ////Tooltip.SetDefault("Fires blue frost flames that explode");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 5.25f;
        Item.value = 650000;
        Item.rare = 6;
        Item.UseSound = SoundID.Item20;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("SpiritFlameCurse").Type;
        Item.shootSpeed = 14f;
    }
    
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "EssenceofEleum", 3);
        recipe.AddIngredient(ItemID.CrystalShard, 15);
        recipe.AddIngredient(ItemID.FrostStaff);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}