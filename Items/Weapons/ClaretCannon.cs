using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class ClaretCannon : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Claret Cannon");
		//Tooltip.SetDefault("Fires bloodfire bullets that drain enemy health");
	}

    public override void SetDefaults()
    {
        Item.damage = 219;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 44;
        Item.height = 24;
        Item.useTime = 3;
        Item.reuseDelay = 10;
        Item.useAnimation = 9;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 5.5f;
        Item.value = 1000000;
        Item.UseSound = SoundID.Item40;
        Item.autoReuse = true;
        Item.shootSpeed = 12f;
        Item.shoot = Mod.Find<ModProjectile>("BloodfireBullet").Type;
        Item.useAmmo = 97;
    }
    
    public override void ModifyTooltips(List<TooltipLine> list)
    {
        foreach (TooltipLine line2 in list)
        {
            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
            {
                line2.OverrideColor = new Color(0, 255, 0);
            }
        }
    }
    
    public override Vector2? HoldoutOffset()
	{
		return new Vector2(0, -5);
	}
    
    public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "BloodstoneCore", 4);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
	}
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("BloodfireBullet").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
    	return false;
	}
}}