using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class TheBallista : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/TheBallista");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("The Ballista");
        Item.damage = 110;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 40;
        Item.height = 62;
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        ////Tooltip.SetDefault("Converts all arrows fired into ballista great arrows\nBallista great arrows are powerful enough to shatter even the toughest armor");
        Item.knockBack = 8f;
        Item.value = 6000000;
        Item.rare = 8;
        Item.UseSound = SoundID.Item5;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("BallistaGreatArrow").Type; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 20f;
        Item.useAmmo = 40;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	Projectile.NewProjectile(source, position, velocity,Mod.Find<ModProjectile>("BallistaGreatArrow").Type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
    	return false;
	}

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Marrow);
        recipe.AddIngredient(ItemID.AncientBattleArmorMaterial);
        recipe.AddIngredient(ItemID.Ectoplasm, 10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}